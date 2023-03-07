using System;
using UnityEngine;

namespace mis.Core
{
    public class BattleStateChangedMessage : IMessage
    {
        public readonly GameId BattleId;
        public readonly BattleState OldState;
        public readonly BattleState NewState;

        public BattleStateChangedMessage(GameId battleId, BattleState oldState, BattleState newState)
        {
            BattleId = battleId;
            OldState = oldState;
            NewState = newState;
        }
    }
}