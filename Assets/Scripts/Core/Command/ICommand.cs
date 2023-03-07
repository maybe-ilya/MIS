using Cysharp.Threading.Tasks;

namespace mis.Core
{
    public interface ICommand
    {
        uint Id { get; }
        CommandState State { get; }
        void Execute();
        UniTask<ICommand> ExecuteAsync();
    }
}
