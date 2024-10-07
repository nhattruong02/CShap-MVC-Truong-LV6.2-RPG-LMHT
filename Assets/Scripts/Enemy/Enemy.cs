using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    NavMeshAgent _agent;
    public float moveRadius = 10f;
    [SerializeField] float waitTime = 2f;
    private float timer;
    bool isMove = true;
    EnemyCombat _enemyCombat;

    [SerializeField] Transform _playerTransform;

    public NavMeshAgent Agent { get => _agent; set => _agent = value; }

    void Start()
    {
        timer = waitTime;
        _agent = GetComponent<NavMeshAgent>();
        _enemyCombat = GetComponent<EnemyCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove && _enemyCombat.isEnemyAlive)
        {
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                MoveToRandomPosition();
                timer = 0f;
            }
        }
    }

    void MoveToRandomPosition()
    {

        Vector3 randomPos = RandomNavmeshLocation(moveRadius);

        _agent.SetDestination(randomPos);
    }

    Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            return hit.position;
        }
        return transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, moveRadius);
    }
}
