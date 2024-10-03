using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    PlayerAnimator _player;
    public IdleState(PlayerAnimator player) {
        _player = player;   
 
    }
    
    public void OnEnter()
    {
        _player.anim.SetFloat(Common.speed, 0);
    }

    public void OnExercute()
    {
        _player.anim.SetFloat(Common.speed, 0);
    }

    public void OnExit()
    {
    }
}
