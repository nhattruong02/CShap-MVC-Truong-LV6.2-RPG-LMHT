using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public enum PlayerAttackType { NormalAttack, Melee, Ranged};
    public PlayerAttackType heroAttackType;

    public GameObject targetedEnemy;
    public float attackRange;
    public float rotateSpeedForAttack;

    private Player playerScript;

    public bool basicAtkIdle = false;
    public bool isPlayerAlive;
    public bool performMeleeAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(targetedEnemy != null)
        {
            if(Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position) > attackRange)
            {
                playerScript.Agent.SetDestination(targetedEnemy.transform.position);
                playerScript.Agent.stoppingDistance = attackRange;

                Quaternion rotationToLookAt = Quaternion.LookRotation(targetedEnemy.transform.position - transform.position);
                float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                    rotationToLookAt.eulerAngles.y,
                    ref playerScript.rotateVelocity,
                    rotateSpeedForAttack * (Time.deltaTime * 5));
                transform.eulerAngles = new Vector3(0, rotationY, 0);
            }
            else
            {
                if(heroAttackType == PlayerAttackType.NormalAttack)
                {
                    if (performMeleeAttack)
                    {

                    }
                }
            }
        }
    }
}
