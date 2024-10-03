using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieStateEnemy : IState
{
    EnemyAnimator _enemy;
    float _health;
    public DieStateEnemy(EnemyAnimator enemy, float health)
    {
        _enemy = enemy;
        this._health = health;

    }
    public void OnEnter()
    {
        
    }

    public void OnExercute()
    {
        if(_health <= 0)
        {
            _enemy.anim.SetTrigger(Common.die);
        }
    }

    public void OnExit()
    {

    }
}
