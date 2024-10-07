using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class Player : MonoBehaviour
{
    NavMeshAgent _agent;
    public float rotateSpeedMovement = 0.1f;
    public float rotateVelocity;
    private PlayerCombat _playerCombatScipt;
    public NavMeshAgent Agent { get => _agent; private set => _agent = value; }

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();    
        _playerCombatScipt = GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerCombatScipt.targetedEnemy != null)
        {
            if (_playerCombatScipt.targetedEnemy.GetComponent<PlayerCombat>() != null)
            {
                if (_playerCombatScipt.targetedEnemy.GetComponent<PlayerCombat>().isPlayerAlive)
                {
                    _playerCombatScipt.targetedEnemy = null;
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && _playerCombatScipt.isPlayerAlive)
        {
            RaycastHit hit;
            // Check if raycast hit st 
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == "Ground")
                {
                    //MOVEMENT
                    _agent.SetDestination(hit.point);
                    _playerCombatScipt.targetedEnemy = null;
                    _agent.stoppingDistance = 0;

                    //ROTATION
                    Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
                    float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                        rotationToLookAt.eulerAngles.y,
                        ref rotateVelocity,
                        rotateSpeedMovement * (Time.deltaTime * 5));
                    transform.eulerAngles = new Vector3(0, rotationY, 0);
                }
            }
        }
    }
}
