using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public StateManager Instant;
    [SerializeField] IState _currentState;
    private void Awake()
    {
        Instant = this;
        DontDestroyOnLoad(this.gameObject);
    }
    public void ChangeState(IState state)
    {
        if (_currentState != null && state.GetType() == _currentState.GetType())
        {
            return;
        }
        if(_currentState != null)
        {
            _currentState.OnExit();
        }
        _currentState = state;
        if(_currentState != null) {
            _currentState.OnEnter();
        }
    }
    private void Update()
    {
        if(_currentState != null)
        {
            _currentState.OnExercute();
        }
    }
}
