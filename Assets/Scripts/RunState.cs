using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState
{
    PlayerAnimator _controller;
    private float _speed;
    public RunState(PlayerAnimator player, float speed)
    {
        _controller = player;
        _speed = speed;

    }
    public void OnEnter()
    {
        _controller.anim.SetFloat(Common.speed, _speed);
    }

    public void OnExercute()
    {
        if(_speed > 0)
        {
            _controller.anim.SetFloat(Common.speed, _speed);
        }
    }

    public void OnExit()
    {
    }
}
