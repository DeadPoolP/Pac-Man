using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public float speed;
    private Rigidbody _rigidbody;
    private Transform _transform;
    public bool validDirection;

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
        NonStopMove();
    }

    public void NonStopMove()
    {
        if (CheckDirection(_transform.forward))
            _rigidbody.MovePosition(_transform.position + speed * Time.deltaTime * _transform.forward);
    }

    public void Up()
    {
        Rotate(0f);
    }

    public void Down()
    {
        Rotate(180f);
    }

    public void Right()
    {
        Rotate(90f);
    }

    public void Left()
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
        if (Physics.Raycast(_transform.position, direction, out hit, 0.55f))
        {
            return (!hit.transform.CompareTag("Wall"));
        }
        return true;
    }
}
