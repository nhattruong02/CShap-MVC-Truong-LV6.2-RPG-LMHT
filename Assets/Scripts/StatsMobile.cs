using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StatsMobile : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public float attackDmg;
    [SerializeField] float attackSpeed;
    public float attackTime;
    private PlayerCombatMobile playerCombat;
    private EnemyCombatMobile enemyCombat;
    // Start is called before the first frame update
    void Start()
    {
        playerCombat = this.GetComponent<PlayerCombatMobile>();
        enemyCombat = this.GetComponent<EnemyCombatMobile>();
    }

    public void TakeDamage(GameObject target, float damage)
    {
        target.GetComponent<StatsMobile>().health -= damage;
        if (target.GetComponent<StatsMobile>().health <= 0 && target.CompareTag(Common.enemy))
        {
            target.gameObject.GetComponent<Animator>().SetTrigger(Common.die);
            target.gameObject.GetComponent<EnemyCombatMobile>().isEnemyAlive = false;
            playerCombat.targetedEnemy = null;
            target.gameObject.GetComponent<EnemyCombatMobile>().performNomalAttack = false;
            StartCoroutine(DestroyAfterTime(target));
        }
        if (target.GetComponent<StatsMobile>().health <= 0 && target.CompareTag(Common.player))
        {
            enemyCombat.targetedPlayer = null;
            target.gameObject.GetComponent<PlayerCombatMobile>().performNormalAttack = false;
            target.gameObject.GetComponent<PlayerCombatMobile>().isPlayerAlive = false;
            target.gameObject.GetComponent<Animator>().SetTrigger(Common.die);
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
            player.GetComponent<StatsMobile>().health += hp;
    }
}
