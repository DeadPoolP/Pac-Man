﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderBehavior : StateMachineBehaviour
{
    private Vector3 _target;
    private Ghost _ghost;
    private bool _onPatrol;
    private GameObject _player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _ghost = animator.gameObject.GetComponent<Ghost>();
        _ghost.SwapMaterial(_ghost.original);
        _ghost.speed = _ghost.wanderingSpeed;
        _ghost.ResetPatrol();
        _target = _ghost.GetCurrentPatrolPoint().position;
        _onPatrol = false;
        _ghost.SetReady(false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (_onPatrol)
        {
            Patrol();
        }
        else
        {
            GoToPatrol();
        }
        if (_player.GetComponent<PlayerController>().godmode)
            _ghost.SetFrightened(true);
    }

    /// <summary>
    /// Patrol sub state
    /// </summary>
    public void Patrol()
    {
        if (_ghost.transform.position != _target) // if not arrived to patrol waypoint
        {
            _ghost.MoveToTarget(_target); // keep going
        }
        else // select next waypoint
        {
            _target = _ghost.GetNextPatrolPoint().position;
            _ghost.targetIntersection = _ghost.GetCurrentPatrolPoint();
        }
    }

    /// <summary>
    /// Wander sub state with goal to go on patrol
    /// </summary>
    public void GoToPatrol()
    {
        if (_ghost.transform.position != _target) // if not arrived to patrol 1st waypoint
        {
            if (_ghost.transform.position == _ghost.targetIntersection.position) // if on an intersection
            {
                Vector3[] directions = _ghost.GetDirection(_target); // Get 2 priority directions for target
                Transform nextIntersection = _ghost.DecideNextIntersection(directions,false); // Decide between those directions; if none can be taken, take a random one between the rest of the direction
                if (nextIntersection == null) // Error case
                {
                    Debug.Log("Error couldnt find path");
                }
                else // Select next intersection
                {
                    _ghost.targetIntersection = nextIntersection;
                    _ghost.SetCurrentDirection(nextIntersection);
                }
            }
            else // Move to targeted intersection
            {
                _ghost.MoveToTarget(_ghost.targetIntersection.position);
            }
        }
        else // if arrived to patrol 1st waypoint, start patrol
        {
            _onPatrol = true;
        }
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
