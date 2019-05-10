using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int dotsEaten = 0;
    private PlayerMotor _playerMotor;

    private Vector3 _up = new Vector3(0f, 0f, 1f);
    private Vector3 _down = new Vector3(0f, 0f, -1f);
    private Vector3 _right = new Vector3(1f, 0f, 0f);
    private Vector3 _left = new Vector3(-1f, 0f, 0f);

    void Awake()
    {
        _playerMotor = GetComponent<PlayerMotor>();
        dotsEaten = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Up") && _playerMotor.CheckDirection(_up))
        {
            _playerMotor.MoveUp();
        }
        if (Input.GetButtonDown("Down") && _playerMotor.CheckDirection(_down))
        {
            _playerMotor.MoveDown();
        }
        if (Input.GetButtonDown("Right") && _playerMotor.CheckDirection(_right))
        {
            _playerMotor.MoveRight();
        }
        if (Input.GetButtonDown("Left") && _playerMotor.CheckDirection(_left))
        {
            _playerMotor.MoveLeft();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("Dot"))
            {
                Dot currentDot = other.gameObject.GetComponent<Dot>();
                dotsEaten++;
            }
        }
    }    
        
    
}
