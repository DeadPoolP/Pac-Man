using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private PlayerMotor _playerMotor;

    public Vector3 Up = new Vector3(0f, 0f, 1f);
    public Vector3 Down = new Vector3(0f, 0f, -1f);
    public Vector3 Right = new Vector3(1f, 0f, 0f);
    public Vector3 Left = new Vector3(-1f, 0f, 0f);
    void Awake()
    {
        _playerMotor = GetComponent<PlayerMotor>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Up") && _playerMotor.CheckDirection(Up))
        {
            _playerMotor.Up();
        }
        if (Input.GetButtonDown("Down") && _playerMotor.CheckDirection(Down))
        {
            _playerMotor.Down();
        }
        if (Input.GetButtonDown("Right") && _playerMotor.CheckDirection(Right))
        {
            _playerMotor.Right();
        }
        if (Input.GetButtonDown("Left") && _playerMotor.CheckDirection(Left))
        {
            _playerMotor.Left();
        }
    }
}
