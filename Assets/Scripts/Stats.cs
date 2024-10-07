using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Stats : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public float attackDmg;
    [SerializeField] float attackSpeed;
    public float attackTime;
    private Animator animator;
    private PlayerCombat playerCombat;
    private EnemyCombat enemyCombat;
    // Start is called before the first frame update
    void Start()
    {
        playerCombat = GameObject.FindGameObjectWithTag(Common.player).GetComponent<PlayerCombat>();
        enemyCombat = GameObject.FindGameObjectWithTag(Common.enemy).GetComponent<EnemyCombat>();
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(GameObject target, float damage)
    {
        target.GetComponent<Stats>().health -= damage;
        if (target.GetComponent<Stats>().health <= 0 && target.CompareTag(Common.enemy))
        {
            target.gameObject.GetComponent<Animator>().SetTrigger(Common.die);
            enemyCombat.isEnemyAlive = false;
            playerCombat.targetedEnemy = null;
            enemyCombat.performNomalAttack = false;
            StartCoroutine(DestroyAfterTime(target));
        }
        if (target.GetComponent<Stats>().health <= 0 && target.CompareTag(Common.player))
        {
            target.gameObject.GetComponent<Animator>().SetTrigger(Common.die);
            enemyCombat.targetedPlayer = null;
            playerCombat.performNormalAttack = false;
            playerCombat.isPlayerAlive = false;
        }

    }
    IEnumerator DestroyAfterTime(GameObject target)
    {
        yield return new WaitForSeconds(2);
        Destroy(target);
    }
    public void RecoveryHPPlayer(GameObject player, float hp)
    {
        if (health >= maxHealth)
            health = maxHealth;
        if (health < maxHealth && health > 0)
            player.GetComponent<Stats>().health += hp;
    }
}
