using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatMobile : MonoBehaviour
{
    [SerializeField] float rotateVelocity;
    public GameObject targetedPlayer;
    public float attackRange;
    public float rotateSpeedForAttack;

    private EnemyMobile _enemyScript;
    public StatsMobile statsScript;
    private Animator _animator;

    public bool isEnemyAlive = true;
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
        _enemyScript = GetComponent<EnemyMobile>();
        statsScript = GetComponent<StatsMobile>();
        _animator = GetComponent<Animator>();
   
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnemyAlive && targetedPlayer != null)
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

    }

    IEnumerator NormalAttackInterval()
    {
        performNomalAttack = false;
        _animator.SetTrigger(Common.normalAttack);
        if (targetedPlayer == null)
        {
            performNomalAttack = false;
        }
        yield return new WaitForSeconds(statsScript.attackTime / ((100 + statsScript.attackTime) * 0.01f));
        performNomalAttack = true;

    }

    //event animator
    public void NormalAttack()
    {
        if (targetedPlayer != null)
        {
            if (targetedPlayer.GetComponent<Targetable>().CompareTag(Common.player))
            {
                statsScript.TakeDamage(targetedPlayer, statsScript.attackDmg);
                playerAnimator.SetTrigger(Common.getHit);
            }
        }
        performNomalAttack = true;

    }
}
