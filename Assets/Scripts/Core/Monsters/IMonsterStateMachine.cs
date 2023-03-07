namespace mis.Core
{
    public interface IMonsterStateMachine
    {
        void ChangeState(IMonsterState state);
    }
}