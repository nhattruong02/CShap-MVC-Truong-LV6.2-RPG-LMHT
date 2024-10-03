using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState
{
    PlayerAnimator _player;
    private float _speed;
    public RunState(PlayerAnimator player, float speed)
    {
        _player = player;
        _speed = speed;

    }
    public void OnEnter()
    {
        _player.anim.SetFloat(Common.speed, _speed);
    }

    public void OnExercute()
    {
        if(_speed > 0)
        {
            _player.anim.SetFloat(Common.speed, _speed);
        }
    }

    public void OnExit()
    {
    }
}
