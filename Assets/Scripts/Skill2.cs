using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2 : MonoBehaviour
{
    [SerializeField] float health;
    public GameObject player;
    // Start is called before the first frame update
    private Stats stats;
    private void OnEnable()
    {
        
        stats = player.GetComponent<Stats>();
    }
    private void Start()
    {
        StartCoroutine(destroyAfterTime());
    }
    private void Update()
    {
       this.transform.position = player.transform.position;
    }

    IEnumerator destroyAfterTime()
    {
        stats.RecoveryHPPlayer(player, health);
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}
