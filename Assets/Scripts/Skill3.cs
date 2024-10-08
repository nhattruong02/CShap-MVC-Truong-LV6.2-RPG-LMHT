using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3 : MonoBehaviour
{
    [SerializeField] float damage;
    public GameObject target;
    // Start is called before the first frame update
    private Stats stats;
    // Update is called once per frame
    private void OnEnable()
    {
        stats = target.GetComponent<Stats>();
    }
    private void Start()
    {
        stats.TakeDamage(target, damage);
    }
    void Update()
    {
        if (target != null)
        {
            this.transform.position = target.transform.position;
            StartCoroutine(destroyAfterTime());
        }
    }
    IEnumerator destroyAfterTime()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
