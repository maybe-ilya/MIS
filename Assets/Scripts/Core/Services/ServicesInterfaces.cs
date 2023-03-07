namespace mis.Core
{
    public interface IStartableService : IService
    {
        int StartPriority { get; }
        void OnServiceStart();
    }
}