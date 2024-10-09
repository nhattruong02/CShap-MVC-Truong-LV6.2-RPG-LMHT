using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMobile : MonoBehaviour
{
    [SerializeField] FixedJoystick _joystick;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] Animator _animator;
    [SerializeField] float _speed;
    NavMeshAgent _agent;
    public float rotateSpeedMovement = 0.1f;
    public float rotateVelocity;
    private PlayerCombatMobile _playerCombatScipt;
    public NavMeshAgent Agent { get => _agent; private set => _agent = value; }

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _playerCombatScipt = GetComponent<PlayerCombatMobile>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerCombatScipt.isPlayerAlive)
        {
            float horizontalMove = _joystick.Horizontal * _speed;
            float verticalMove = _joystick.Vertical * _speed;
            _rigidbody.velocity = new Vector3(horizontalMove, 0, verticalMove);
            if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
            {
                transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
                float currentSpeed = _rigidbody.velocity.magnitude/_speed;
                _animator.SetFloat(Common.speed, currentSpeed);
            }
            else
            {
                _rigidbody.velocity = Vector3.zero;
                _animator.SetFloat(Common.speed, 0);
            }
        }
    }
}

