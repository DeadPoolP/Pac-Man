using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    public float speed = 3;
    public float wanderingSpeed = 3;
    public float chasingSpeed = 4;
    public float fleeingSpeed = 2;
    public LayerMask intersectionLayer;
    public LayerMask playerLayer;
    private Rigidbody _rigidbody;
    private Transform _transform;
    [SerializeField]
    private Transform[] _patrolPoints;
    private Transform[] _intersections;
    public Transform targetIntersection;
    [SerializeField]
    private int currentIndex = 0;
    [SerializeField]
    private float step;
    [SerializeField]
    private Transform spawnPoint;
    public Transform gateEntrance;
    public Transform gateIntersection;

    private Animator _animator;


    public Material original;
    public Material frightened;


    private Vector3 currentDirection;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        intersectionLayer = LayerMask.NameToLayer("Intersection");
        playerLayer = LayerMask.NameToLayer("Player");
        _animator = GetComponent<Animator>();
        SwapMaterial(original);
    }

    // Start is called before the first frame update
    void Start()
    {
        _intersections = new Transform[4];
        GetAllIntersections();
        currentDirection = _transform.forward;
        _animator.SetBool("Dead", false);
        ResetAnimator();
        ResetPatrol();
    }

    public void Die()
    {
        _transform.position = spawnPoint.position;
        ResetAnimator();
        SetDead(true);
        _animator.Play("Dead");
        targetIntersection = gateIntersection;
    }

    public void ResetPatrol()
    {
        currentIndex = 0;
    }

    public void SwapMaterial(Material mat)
    {
        GetComponentInChildren<MeshRenderer>().material = mat;
    }

    public void ResetAnimator()
    {
        SetReady(false);
        SetChasing(false);
        SetFrightened(false);
    }

    public void SetChasing(bool b)
    {
        _animator.SetBool("Chasing", b);
    }
    public void SetReady(bool b)
    {
        _animator.SetBool("Ready", b);
    }
    public void SetFrightened(bool b)
    {
        _animator.SetBool("Frightened", b);
    }
    public void SetDead(bool b)
    {
        _animator.SetBool("Dead", b);
    }

    public Vector3 GetCurrentPatrolPoint()
    {
        return _patrolPoints[currentIndex].position;
    }

    public Vector3 GetNextPatrolPoint()
    {
        currentIndex = (currentIndex + 1) % _patrolPoints.Length;
        return _patrolPoints[currentIndex].position;
    }

    public void MoveToTarget(Vector3 target)
    {
        step = speed * Time.fixedDeltaTime;
        _transform.position = Vector3.MoveTowards(_transform.position, target, step);
    }

    /// <summary>
    /// Calculate the closest direction to the direction (vector) ghost -> target
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public Vector3[] GetDirection(Vector3 target)
    {
        float dot = Vector3.Dot(_transform.forward, target - _transform.position) / (Vector3.Magnitude(target - transform.position));
        float angle = Mathf.Acos(dot);
        if (target.z - _transform.position.z < 0)
        {
            angle *= -1;
        }
        if (Mathf.Abs(Mathf.Sin(angle)) > Mathf.Abs(Mathf.Cos(angle)))
        {
            return new Vector3[2] { new Vector3(0f, 0f, Mathf.Sign(Mathf.Sin(angle))), new Vector3(Mathf.Sign(Mathf.Cos(angle)), 0f, 0f) };
        }
        else
        {
            return new Vector3[2] { new Vector3(Mathf.Sign(Mathf.Cos(angle)), 0f, 0f), new Vector3(0f, 0f, Mathf.Sign(Mathf.Sin(angle))) };
        }
    }

    /// <summary>
    /// Stores all the next intersections visible by the Ghost
    /// </summary>
    public void GetAllIntersections()
    {
        RaycastHit hit;
        int i = 0;
        Vector3[] directions = { _transform.forward, _transform.right, -_transform.right, -_transform.forward };
        foreach (var direction in directions)
        {
            if (Physics.Raycast(_transform.position, direction, out hit, intersectionLayer))
            {
                if (hit.transform.CompareTag("Intersection"))
                {
                    _intersections[i] = hit.transform;
                }
                else
                {
                    _intersections[i] = null;
                }
                i++;
            }
        }
    }

    /// <summary>
    /// Looks in all direction for PacMan
    /// </summary>
    /// <returns>True if pacman is detected, false otherwise</returns>
    public bool LookForPacMan()
    {
        RaycastHit hit;
        int i = 0;
        Vector3[] directions = { _transform.forward, _transform.right, -_transform.right, -_transform.forward };
        foreach (var direction in directions)
        {
            if (Physics.Raycast(_transform.position, direction, out hit, playerLayer))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    return true;
                }
                i++;
            }
        }
        return false;
    }

    /// <summary>
    /// Given the 2 priority directions, decides what direction to go and returns the next intersection
    /// </summary>
    /// <param name="directions"></param>
    /// <returns></returns>
    public Transform DecideNextIntersection(Vector3[] directions)
    {
        GetAllIntersections();
        RaycastHit hit;
        int i = 0;
        while (i < directions.Length)
        {
            if (Physics.Raycast(_transform.position, directions[i], out hit, intersectionLayer))
            {
                Vector3 intent = directions[i];
                Vector3 temp = intent + currentDirection;
                bool backward = !(Vector3.Magnitude(temp) < 0.1f);
                if (hit.transform.CompareTag("Intersection") && backward) // Dont go backward
                {
                    return hit.transform;
                }
            }
            i++;
        }
        int j = 0;
        while (j < _intersections.Length && _intersections[j] == null)
        {
            j++;
        }
        if (j == _intersections.Length)
        {
            Debug.Log("No intersection Found !");
            return null;
        }
        return _intersections[j];
    }


    /// <summary>
    /// Stores the current direction the Ghost is going
    /// </summary>
    /// <param name="intersection"></param>
    public void SetCurrentDirection(Transform intersection)
    {
        currentDirection = Vector3.Normalize(intersection.position - _transform.position);
    }


    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5);
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, transform.position - transform.forward * 5);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * 5);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - transform.right * 5);
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetChasing(false);
        }
    }
}
