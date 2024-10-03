using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateEnemy : IState
{
    EnemyAnimator _enemy;
    public IdleStateEnemy(EnemyAnimator enemy)
    {
        _enemy = enemy;

    }

    public void OnEnter()
    {
        _enemy.anim.SetFloat(Common.speed, 0);
    }

    public void OnExercute()
    {
        _enemy.anim.SetFloat(Common.speed, 0);
    }

    public void OnExit()
    {
    }
}
