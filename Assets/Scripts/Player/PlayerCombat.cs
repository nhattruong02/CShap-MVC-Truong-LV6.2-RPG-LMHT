using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public enum PlayerAttackType { NormalAttack, Melee, Ranged};
    public PlayerAttackType heroAttackType;

    public GameObject targetedEnemy;
    public float attackRange;
    public float rotateSpeedForAttack;

    private Player _playerScript;
    public Stats statsScript;
    private Animator _animator;
   

    public bool basicAtkIdle = false;
    public bool isPlayerAlive;
    public bool performNomalAttack = true;

    [SerializeField] private Animator enemyAnimator;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    // Start is called before the first frame update
    void Start()
    {
        _playerScript = GetComponent<Player>();
        statsScript = GetComponent<Stats>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(targetedEnemy != null)
        {
            if(Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position) > attackRange)
            {
                _playerScript.Agent.SetDestination(targetedEnemy.transform.position);
                _playerScript.Agent.stoppingDistance = attackRange;

                Quaternion rotationToLookAt = Quaternion.LookRotation(targetedEnemy.transform.position - transform.position);
                float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                    rotationToLookAt.eulerAngles.y,
                    ref _playerScript.rotateVelocity,
                    rotateSpeedForAttack * (Time.deltaTime * 5));
                transform.eulerAngles = new Vector3(0, rotationY, 0);
            }
            else
            {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                {
                    if (heroAttackType == PlayerAttackType.NormalAttack)
                    {
                        if (performNomalAttack)
                        {
                            StartCoroutine(NormalAttackInterval());
                        }
                    }
                }

            }
        }
    }

    IEnumerator NormalAttackInterval()
    {
        performNomalAttack = false;
        _animator.SetTrigger(Common.normalAttack);
        if(targetedEnemy == null)
        {
            performNomalAttack = true;
        }
        yield return new WaitForSeconds(statsScript.attackTime / ((100 + statsScript.attackTime) * 0.01f));
        performNomalAttack = true;

    }

    //event animator
    public void NormalAttack()
    {
        if(targetedEnemy != null)
        {
            if (targetedEnemy.GetComponent<Targetable>().CompareTag(Common.enemy))
            {
                targetedEnemy.GetComponent<Stats>().health -= statsScript.attackDmg;
                enemyAnimator.SetTrigger(Common.getHit);
            }
        }
        performNomalAttack = true;
    }
}
