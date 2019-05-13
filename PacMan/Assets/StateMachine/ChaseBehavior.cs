using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehavior : StateMachineBehaviour
{

    private GameObject _player;
    private Vector3 _target;
    private Ghost _ghost;

    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _ghost = animator.gameObject.GetComponent<Ghost>();
        _ghost.SwapMaterial(_ghost.original);
        _player = GameObject.FindGameObjectWithTag("Player");
        _ghost.speed = _ghost.chasingSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _target = _player.transform.position;
        if (_ghost.name == "Pink Ghost")
            _target = _player.transform.position + _player.transform.forward * 4;
        if (_ghost.name == "Blue Ghost")
            _target = _player.transform.position + _player.transform.right * 4;
        if (_ghost.transform.position != _target)
        {
            if (_ghost.transform.position == _ghost.targetIntersection.position)
            {
                Vector3[] directions = _ghost.GetDirection(_target);
                Transform nextIntersection = _ghost.DecideNextIntersection(directions,true);
                if (nextIntersection == null)
                {
                    Debug.Log("Error coudlnt find path");
                }
                else
                {
                    _ghost.targetIntersection = nextIntersection;
                    _ghost.SetCurrentDirection(nextIntersection);
                }
            }
            else
            {
                _ghost.MoveToTarget(_ghost.targetIntersection.position);
            }
        }
        if (_player.GetComponent<PlayerController>().godmode)
            _ghost.SetFrightened(true);
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
