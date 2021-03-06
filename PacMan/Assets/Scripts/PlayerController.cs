﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int dotsEaten = 0;
    public int bigDotsEaten = 0;
    public int killCount = 0;
    public int health = 3;
    public bool godmode = false;
    [SerializeField]
    private float godmodeTime = 5f;
    private float godmodeTimer = 0f;
    [SerializeField]
    private GameObject godmodeFX;
    private PlayerMotor _playerMotor;

    [SerializeField]
    private Transform spawnPoint;


    private Vector3 _up = new Vector3(0f, 0f, 1f);
    private Vector3 _down = new Vector3(0f, 0f, -1f);
    private Vector3 _right = new Vector3(1f, 0f, 0f);
    private Vector3 _left = new Vector3(-1f, 0f, 0f);

    void Awake()
    {
        _playerMotor = GetComponent<PlayerMotor>();
        dotsEaten = 0;
        health = 3;
        godmode = false;
        godmodeFX.SetActive(godmode);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

   

    // Update is called once per frame
    void Update()
    {
        // Player Inputs
        if (Input.GetButtonDown("Up") && _playerMotor.CheckForWallsInDirection(_up))
        {
            _playerMotor.MoveUp();
        }
        if (Input.GetButtonDown("Down") && _playerMotor.CheckForWallsInDirection(_down))
        {
            _playerMotor.MoveDown();
        }
        if (Input.GetButtonDown("Right") && _playerMotor.CheckForWallsInDirection(_right))
        {
            _playerMotor.MoveRight();
        }
        if (Input.GetButtonDown("Left") && _playerMotor.CheckForWallsInDirection(_left))
        {
            _playerMotor.MoveLeft();
        }

        // God mode control
        if (godmode)
        {
            godmodeTimer -= Time.deltaTime;
        }
        if(godmodeTimer < 0)
        {
            godmode = false;
        }
        godmodeFX.SetActive(godmode);
    }

    private void Die()
    {
        health--;
    }

    private void Kill(Ghost ghost)
    {
        killCount++;
        ghost.Die();
    }

    private void Respawn()
    {
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
    }

    /// <summary>
    /// Check collisions with any Dot, BigDot and Ghost (Death hitbox)
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("Dot"))
            {
                Dot currentDot = other.gameObject.GetComponent<Dot>();
                dotsEaten++;
            }
            if (other.gameObject.CompareTag("BigDot"))
            {
                BigDot currentBigDot = other.gameObject.GetComponent<BigDot>();
                bigDotsEaten++;
                godmode = true;
                godmodeTimer = godmodeTime;
            }

            if (other.gameObject.CompareTag("Death"))
            {
                if (godmode)
                {
                    Kill(other.gameObject.GetComponentInParent<Ghost>());
                }
                else
                {
                    Die();
                    Respawn();
                }
            }
        }
    }    
    

    
}
