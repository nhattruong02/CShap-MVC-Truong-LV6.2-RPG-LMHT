using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(StateManager))]
public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] StateManager _stateManager;
    NavMeshAgent agent;
    public Animator anim;
    private Stats _stats;

    private float speed;
    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
        _stateManager = this.GetComponent<StateManager>();
        _stats = this.GetComponent<Stats>();

    }
    private void Update()
    {
        speed = agent.velocity.magnitude / agent.speed;
        _stateManager.ChangeState(new IdleStateEnemy(this));
        _stateManager.ChangeState(new RunStateEnemy(this, speed));
/*        _stateManager.ChangeState(new DieStateEnemy(this, _stats.health));
*/    }

}
