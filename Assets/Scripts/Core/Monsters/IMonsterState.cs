namespace mis.Core
{
    public interface IMonsterState
    {
        void OnEnterState();
        void OnUpdateState();
        void OnExitState();
    }
}