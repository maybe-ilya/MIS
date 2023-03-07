using Cysharp.Threading.Tasks;
using System.Threading;

namespace mis.Core
{
    public static class UniTaskUtils
    {
        public static UniTask DelayForSeconds(float seconds, CancellationTokenSource cancellationTokenSource) =>
            UniTask.Delay((int)(seconds * 1000), cancellationToken: cancellationTokenSource.Token);
    }
}