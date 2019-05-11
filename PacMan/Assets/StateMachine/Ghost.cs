using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    public float speed = 3;
    private Rigidbody _rigidbody;
    private Transform _transform;
    [SerializeField]
    private Transform[] _patrolPoints;
    private Transform[] intersections;
    public Transform targetIntersection;
    [SerializeField]
    private int currentIndex = 0;
    [SerializeField]
    private float wallDetectionRange = 0.5f;
    [SerializeField]
    private float step;

    public Vector3 currentDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        intersections = new Transform[4];
        GetAllIntersections();
        currentDirection = _transform.forward;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetPatrol()
    {
        currentIndex = 0;
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
    public void NonStopMove()
    {
        _rigidbody.MovePosition(_transform.position + speed * Time.fixedDeltaTime * _transform.forward);
    }
    /*public void Rotate(Vector3 direction)
    {
        float angle = Vector3.SignedAngle(_transform.forward, direction, Vector3.up);
        _transform.rotation = Quaternion.Euler(0f, angle + _transform.rotation.eulerAngles.y, 0f);

    }*/

    public Vector3 GetDirection(Vector3 target)
    {
        float angle = Mathf.Sign(target.z - _transform.position.z) * Mathf.Acos(Vector3.Dot(_transform.position, target) / (Vector3.SqrMagnitude(_transform.position) * Vector3.SqrMagnitude(target)));
        if (Mathf.Abs(Mathf.Sin(angle)) > Mathf.Abs(Mathf.Cos(angle)))
        {
            return new Vector3(0f, 0f, Mathf.Sign(Mathf.Sin(angle)));
        } 
        else
        {
            return new Vector3(Mathf.Sign(Mathf.Cos(angle)), 0f, 0f);
        }
    }

    public bool CheckDirection(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(_transform.position, direction, out hit, wallDetectionRange))
        {
            return !(hit.transform.CompareTag("Wall"));
        }
        return true;
    }

    public void GetAllIntersections()
    {
        RaycastHit hit;
        int i = 0;
        Vector3[] directions = { _transform.forward, _transform.right, -_transform.right, -_transform.forward };
        foreach (var direction in directions)
        {
            if (Physics.Raycast(_transform.position, direction, out hit))
            {
                if (hit.transform.CompareTag("Intersection"))
                {
                    intersections[i] = hit.transform;
                }
                else
                {
                    intersections[i] = null;
                }
                i++;
            }
        }
    }

    public void GetAllIntersectionsExceptBackward()
    {
        RaycastHit hit;
        int i = 0;
        Vector3[] directions = { _transform.forward, -_transform.right, _transform.right, -_transform.forward };
        foreach (var direction in directions)
        {
            if(direction == -currentDirection)
            {
                intersections[i] = null;
            }
            else
            {
                if (Physics.Raycast(_transform.position, direction, out hit))
                {
                    if (hit.transform.CompareTag("Intersection"))
                    {
                        intersections[i] = hit.transform;
                    }
                    else
                    {
                        intersections[i] = null;
                    }
                    i++;
                }
            }
            
        }
    }

    public Transform DecideNextIntersection(Vector3 direction)
    {
        GetAllIntersectionsExceptBackward();      
        RaycastHit hit;
        if (Physics.Raycast(_transform.position, direction, out hit))
        {
            if (hit.transform.CompareTag("Intersection"))
            {
                return hit.transform;
            }
            else
            {
                int i = 0;
                while (i < intersections.Length && intersections[i] == null)
                {
                    i++;
                }
                if (i == intersections.Length)
                {
                    Debug.Log("No intersection Found !");
                    return null;
                }
                return intersections[i];

            }
        }
        Debug.Log("No intersection Found !");
        return null;
    }

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
        Gizmos.DrawLine(_transform.position, transform.position - transform.right * 5);
    }
}
