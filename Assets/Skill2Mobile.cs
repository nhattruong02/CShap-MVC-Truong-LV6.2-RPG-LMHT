using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2Mobile : MonoBehaviour
{
    [SerializeField] float health;
    public GameObject player;
    // Start is called before the first frame update
    private StatsMobile stats;
    private void OnEnable()
    {

        stats = player.GetComponent<StatsMobile>();
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
