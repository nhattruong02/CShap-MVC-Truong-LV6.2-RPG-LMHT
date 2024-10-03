using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] float rotateVelocity;
    public Transform targetedPlayer;
    public float attackRange;
    public float rotateSpeedForAttack;

    private Enemy _enemyScript;
    public Stats statsScript;
    private Animator _animator;

    public bool isPlayerAlive;
    public bool performNomalAttack = true;

    [SerializeField] private Animator playerAnimator;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    // Start is called before the first frame update
    void Start()
    {
        _enemyScript = GetComponent<Enemy>();
        statsScript = GetComponent<Stats>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.gameObject.transform.position, targetedPlayer.transform.position) < _enemyScript.moveRadius)
        {
            _enemyScript.Agent.SetDestination(targetedPlayer.transform.position);
            _enemyScript.Agent.stoppingDistance = attackRange;

            Quaternion rotationToLookAt = Quaternion.LookRotation(targetedPlayer.transform.position - transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                rotationToLookAt.eulerAngles.y,
                ref rotateVelocity,
                rotateSpeedForAttack * (Time.deltaTime * 5));
            transform.eulerAngles = new Vector3(0, rotationY, 0);
            if (Vector3.Distance(this.gameObject.transform.position, targetedPlayer.transform.position) < attackRange)
            {
                if (performNomalAttack)
                {
                    StartCoroutine(NormalAttackInterval());
                }
            }
        }

    }

    IEnumerator NormalAttackInterval()
    {
        performNomalAttack = false;
        _animator.SetTrigger(Common.normalAttack);
        if (targetedPlayer == null)
        {
            performNomalAttack = true;
        }
        yield return new WaitForSeconds(statsScript.attackTime / ((100 + statsScript.attackTime) * 0.01f));
        performNomalAttack = true;

    }

    //event animator
    public void NormalAttack()
    {
        if (targetedPlayer != null)
        {
            if (targetedPlayer.CompareTag(Common.player))
            {
                targetedPlayer.GetComponent<Stats>().health -= statsScript.attackDmg;
                playerAnimator.SetTrigger(Common.getHit);
            }
        }
        performNomalAttack = true;
    }
}
