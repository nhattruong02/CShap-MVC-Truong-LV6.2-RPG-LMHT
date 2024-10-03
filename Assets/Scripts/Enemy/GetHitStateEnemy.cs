using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GetHitStateEnemy : IState
{
    EnemyAnimator _enemy;
    public GetHitStateEnemy(EnemyAnimator enemy)
    {
        _enemy = enemy;

    }
    public void OnEnter()
    {
        _enemy.anim.SetTrigger(Common.getHit);
    }

    public void OnExercute()
    {
        _enemy.anim.SetTrigger(Common.getHit);
    }

    public void OnExit()
    {
        
    }
}
