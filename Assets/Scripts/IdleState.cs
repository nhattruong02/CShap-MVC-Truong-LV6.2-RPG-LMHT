using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    PlayerAnimator _controller;
    public IdleState(PlayerAnimator player) {
        _controller = player;   
 
    }
    
    public void OnEnter()
    {
        _controller.anim.SetFloat(Common.speed, 0);
    }

    public void OnExercute()
    {
        _controller.anim.SetFloat(Common.speed, 0);
    }

    public void OnExit()
    {
    }
}
