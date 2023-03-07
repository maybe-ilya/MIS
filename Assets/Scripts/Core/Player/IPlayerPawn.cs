using UnityEngine;
using static mis.Core.UnityInputActions;

namespace mis.Core
{
    public interface IPlayerPawn :
        IPlayerActions,
        IGameEntityComponent
    {
        void OnGainControl();
        void OnLoseControl();
        void TeleportTo(Transform target);
    }
}