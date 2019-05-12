using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private float speed = 4f;

    [SerializeField]
    private float wallDetectionRange = 0.8f;

    private Rigidbody _rigidbody;
    private Transform _transform;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (CheckDirection(_transform.forward))
        {
            NonStopMove();

        }
        
    }

    public void NonStopMove()
    {
        _rigidbody.MovePosition(_transform.position + speed * Time.fixedDeltaTime * _transform.forward);
    }

    public void MoveUp()
    {
        Rotate(0f);
    }

    public void MoveDown()
    {
        Rotate(180f);
    }

    public void MoveRight()
    {
        Rotate(90f);
    }

    public void MoveLeft()
    {
        Rotate(-90f);
    }

    public void Rotate(float yAngle)
    {
        _transform.rotation = Quaternion.Euler(0f, yAngle, 0f);
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

    void OnDrawGizmosSelected()
    {
            
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * wallDetectionRange);
        
    }
}
