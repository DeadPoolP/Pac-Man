using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeingBehavior : StateMachineBehaviour
{

    private GameObject _player;
    private Ghost _ghost;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _ghost = animator.GetComponent<Ghost>();
        _ghost.SwapMaterial(_ghost.frightened);
        _ghost.speed = _ghost.fleeingSpeed;
        _player = GameObject.FindGameObjectWithTag("Player");

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_ghost.transform.position == _ghost.targetIntersection.position) // if on an intersection
        {
            Vector3[] directions = _ghost.GetDirection(_player.transform.position); // Get 2 priority directions for target
            for (int i = 0; i < directions.Length; i++)
            {
                directions[i] = -directions[i]; // inverse direction because ghost is fleeing not chasing
            }
            Transform nextIntersection = _ghost.DecideNextIntersection(directions,true); // Decide between those directions; if none can be taken, take a random one between the rest of the direction
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
        if (!_player.GetComponent<PlayerController>().godmode) // Check if player is in godmode, if yes proceed to Frightened State
        {
            _ghost.SetFrightened(false);
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
