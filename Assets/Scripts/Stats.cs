using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public float attackDmg;
    [SerializeField] float attackSpeed;
    public float attackTime;
    PlayerCombat playerCombat;


    // Start is called before the first frame update
    void Start()
    {
        playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
/*            Destroy(gameObject);
*/            playerCombat.targetedEnemy = null;
            playerCombat.performNomalAttack = false;
        }
    }
}
