using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombatMobile : MonoBehaviour
{
    public enum PlayerAttackType { NormalAttack, Ranged, Heath, Mage };
    public PlayerAttackType heroAttackType;

    public GameObject targetedEnemy;
    public float attackRange;
    public float rotateSpeedForAttack;

    private PlayerMobile _playerScript;
    public StatsMobile statsScript;
    private Animator _animator;
    public bool basicAtkIdle = false;
    public bool isPlayerAlive = true;
    public bool performNormalAttack = true;

    [SerializeField] private Animator enemyAnimator;

    [Header("Ranged Variables")]
    public bool performRangedAttack = true;
    [SerializeField] GameObject rangedPrefab;
    [SerializeField] Transform spawnSkill1;
    [SerializeField] float attackRanged;

    [Header("Mage Variables")]
    public bool performMage = true;
    [SerializeField] GameObject magePrefab;
    [SerializeField] float attackMage;

    [Header("Health Variables")]
    public bool performHealth = true;
    [SerializeField] GameObject healthPrefab;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    // Start is called before the first frame update
    void Start()
    {
        _playerScript = GetComponent<PlayerMobile>();
        statsScript = GetComponent<StatsMobile>();
        _animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (targetedEnemy != null && isPlayerAlive)
        {
            if (heroAttackType == PlayerAttackType.Ranged)
            {
                if (performRangedAttack)
                {
                    attackRange = attackRanged;
                }
            }
            if (heroAttackType == PlayerAttackType.Mage)
            {
                if (performMage)
                {
                    attackRange = attackRanged;
                }
            }
        }
    }

    private void lookAtEnemy()
    {
        Quaternion rotationToLookAt = Quaternion.LookRotation(targetedEnemy.transform.position - transform.position);
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
            rotationToLookAt.eulerAngles.y,
            ref _playerScript.rotateVelocity,
            rotateSpeedForAttack * (Time.deltaTime * 5));
        transform.eulerAngles = new Vector3(0, rotationY, 0);
    }

    public void NormalAttackButton()
    {
        if (targetedEnemy != null && isPlayerAlive)
        {
            if (heroAttackType == PlayerAttackType.NormalAttack)
            {
                attackRange = 4;
                performNormalAttack = true;
                if (Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position) < attackRange)
                {
                    if (performNormalAttack)
                    {
                        StartCoroutine(NormalAttackInterval());
                        lookAtEnemy();
                    }
                }
            }
        }
    }

    public IEnumerator HealthInterval()
    {
        performHealth = false;
        _animator.SetTrigger(Common.recoverHP);
        yield return new WaitForSeconds(statsScript.attackTime / ((100 + statsScript.attackTime) * 0.01f));
        performHealth = true;
    }

    public IEnumerator MageAttackInterval()
    {
        performMage = false;
        _animator.SetTrigger(Common.mageAttack);
        if (targetedEnemy == null)
        {
            performMage = false;
        }
        yield return new WaitForSeconds(statsScript.attackTime / ((100 + statsScript.attackTime) * 0.01f));
        performMage = true;
    }

    public IEnumerator RangedAttackInterval()
    {
        performRangedAttack = false;
        _animator.SetTrigger(Common.rangedAttack);
        if (targetedEnemy == null)
        {
            performRangedAttack = false;
        }
        yield return new WaitForSeconds(statsScript.attackTime / ((100 + statsScript.attackTime) * 0.01f));
        performRangedAttack = true;
    }

    IEnumerator NormalAttackInterval()
    {
        performNormalAttack = false;
        _animator.SetTrigger(Common.normalAttack);
        if (targetedEnemy == null)
        {
            performNormalAttack = false;
        }
        yield return new WaitForSeconds(statsScript.attackTime / ((100 + statsScript.attackTime) * 0.01f));
        performNormalAttack = true;

    }

    //event animator
    public void NormalAttack()
    {
        if (targetedEnemy != null)
        {
            if (targetedEnemy.GetComponent<Targetable>().CompareTag(Common.enemy))
            {
                statsScript.TakeDamage(targetedEnemy, statsScript.attackDmg);
            }
        }
        performNormalAttack = true;
    }

    //event animator
    public void RangedAttack()
    {
        if (targetedEnemy != null)
        {
            if (targetedEnemy.GetComponent<Targetable>().CompareTag(Common.enemy))
            {
                _playerScript.Agent.stoppingDistance = attackRange;
                spawnRangedAttack(targetedEnemy);
            }
        }
        performRangedAttack = true;
    }

    //event animator
    public void MageAttack()
    {
        if (targetedEnemy != null)
        {
            if (targetedEnemy.GetComponent<Targetable>().CompareTag(Common.enemy))
            {
                spawnMageAttack(targetedEnemy);
            }
        }
        performRangedAttack = true;
    }

    //event animator
    public void RecoveryHP()
    {
        healthPrefab.GetComponent<Skill2Mobile>().player = _playerScript.gameObject;
        performHealth = true;
        Instantiate(healthPrefab);
    }



    private void spawnMageAttack(GameObject targetedEnemy)
    {
        magePrefab.GetComponent<Skill3Mobile>().target = targetedEnemy;
        Instantiate(magePrefab);
    }

    private void spawnRangedAttack(GameObject targetedEnemy)
    {
        rangedPrefab.GetComponent<Skill1Mobile>().target = targetedEnemy;
        Instantiate(rangedPrefab, spawnSkill1.transform.position, Quaternion.identity);

    }
}
