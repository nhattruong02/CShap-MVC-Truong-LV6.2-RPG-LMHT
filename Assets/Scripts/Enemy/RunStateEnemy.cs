using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunStateEnemy : IState
{
    EnemyAnimator _enemy;
    private float _speed;
    public RunStateEnemy(EnemyAnimator enemy, float speed)
    {
        _enemy = enemy;
        _speed = speed;

    }
    public void OnEnter()
    {
        _enemy.anim.SetFloat(Common.speed, _speed);
    }

    public void OnExercute()
    {
        if (_speed > 0)
        {
            _enemy.anim.SetFloat(Common.speed, _speed);
        }
    }

    public void OnExit()
    {
    }
}
