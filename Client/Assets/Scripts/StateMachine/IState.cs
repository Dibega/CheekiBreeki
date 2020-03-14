public interface IState
{
    EStateId StateID { get; }
    void Enter();
    void Exit();
    void Update();
}
