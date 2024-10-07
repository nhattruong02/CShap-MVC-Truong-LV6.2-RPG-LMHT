using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    PlayerAnimator _controller;
    PlayerCombat _playerCombat;
    public AttackState(PlayerAnimator player,PlayerCombat playerCombat)
    {
        _controller = player;
        _playerCombat = playerCombat;
    }
    public void OnEnter()
    {
        _controller.anim.SetTrigger(Common.normalAttack);
    }

    public void OnExercute()
    {
        _playerCombat.performNormalAttack = false;
        _controller.anim.SetTrigger(Common.normalAttack);
        if (_playerCombat.targetedEnemy == null)
        {
            _playerCombat.performNormalAttack = true;
        }
        if(_playerCombat.statsScript.attackTime / ((100 + _playerCombat.statsScript.attackTime) * 0.01f) <= 0.1f)
        {
            _playerCombat.performNormalAttack = true;
        }
    }

    public void OnExit()
    {
        
    }
}
