using System.Collections.Generic;

public class StateMachine
{
    private Dictionary<EStateId, IState> _states = new Dictionary<EStateId, IState>();

    private IState _previousState;
    private IState _nowState;

    public void Update()
    {
        _nowState.Update();
    }

    public void ChangeState(EStateId eStateId)
    {
        IState localState;
        if (_states.TryGetValue(eStateId, out localState))
        {
            if (_nowState != localState)
            {
                if (_nowState != null)
                {
                    _previousState = _nowState;
                    if (_previousState != null)
                    {
                        _previousState.Exit();
                    }
                }
                _nowState = localState;
                _nowState.Enter();
            }
        }
    }

    public void AddState(EStateId eStateId, IState state)
    {
        if (!_states.ContainsKey(eStateId))
            _states.Add(eStateId, state);
    }
}
