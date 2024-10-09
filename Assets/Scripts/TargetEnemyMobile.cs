using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEnemyMobile : MonoBehaviour
{

    List<GameObject> enemies = new List<GameObject>();
    [SerializeField] PlayerCombatMobile _playerCombat;
    [SerializeField] Transform _player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Common.enemy))
        {
            enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Common.enemy) && !other.GetComponent<EnemyCombatMobile>().isEnemyAlive )
        {
            enemies.Remove(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Common.enemy))
        {
            enemies.Remove(other.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(enemies != null) {
            if(enemies.Count == 1)
            {
                _playerCombat.targetedEnemy = enemies[0];
            }
            if(enemies.Count > 1) 
            { 
                _playerCombat.targetedEnemy = FindClosestEnemy(_player.position, enemies);
            }
        }  
    }
    public GameObject FindClosestEnemy(Vector3 playerPosition, List<GameObject> enemies)
    {
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(playerPosition, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }
        return closestEnemy;
    }
}
