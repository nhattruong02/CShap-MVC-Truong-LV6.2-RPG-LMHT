using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(StateManager))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] StateManager _stateManager;
    NavMeshAgent agent;
    public Animator anim;

    private float speed;
    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
        _stateManager = this.GetComponent<StateManager>();

    }
    private void Update()
    {
        speed = agent.velocity.magnitude / agent.speed;
        _stateManager.ChangeState(new IdleState(this));
        _stateManager.ChangeState(new RunState(this, speed));
    }
/*    private void ChangeAnimation(float speed)
    {
        if(speed == 0)
        {
            _stateManager.ChangeState(new IdleState(this));
        }
        else
        {
            _stateManager.ChangeState(new RunState(this,speed));
        }
    }*/
}
