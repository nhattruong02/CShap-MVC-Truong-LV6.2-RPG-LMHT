using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill1 : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] int speed;
    public GameObject target;
    // Start is called before the first frame update
    private Stats stats;
    // Update is called once per frame
    private void OnEnable()
    {
        stats = target.GetComponent<Stats>();
    }
    void Update()
    {
        if(target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            if(transform.position == target.transform.position )
            {
                stats.TakeDamage(target, damage);
                Destroy(gameObject);
            }
        }
    }
}
