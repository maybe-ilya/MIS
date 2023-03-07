using mis.Core;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace mis.Player
{
    public sealed class PlayerService : IPlayerService, IStartableService
    {
        private readonly IObjectService _objectService;
        private readonly IMessageService _messageService;
        private readonly List<IPlayer> _players;     // TODO: splitscreen 

        public int StartPriority => 0;

        public PlayerService(IObjectService objectService, IMessageService messageService)
        {
            _objectService = objectService;
            _messageService = messageService;
            _players = new List<IPlayer>();
        }

        public void OnServiceStart()
        {
            _messageService.Subscribe<SwitchLevelMessage>(OnSwitchLevel);
        }

        public IPlayer CreateNewPlayer()
        {
            var index = _players.Count;
            var newPlayer = new Player(index);
            var newPlayerController = CreateNewPlayerController(index);

            newPlayer.Control(newPlayerController);
            _players.Add(newPlayer);

            _messageService.Send(new PlayerSpawnedMessage(newPlayer));

            return newPlayer;
        }

        public IPlayer GetPlayer(int index) =>
            _players[index];

        public IPlayer GetFirstPlayer() =>
            GetPlayer(0);

        private PlayerController CreateNewPlayerController(int index)
        {
            var gameObject = new GameObject($"Player Controller #{index}");
            var playerController = gameObject.AddComponent<PlayerController>();
            gameObject.AddComponent<InputController>();
            Object.DontDestroyOnLoad(gameObject);

            return playerController;
        }

        public IPlayerController GetPlayerController(int index) =>
            GetPlayer(index)?.Controller;

        public IPlayerController GetFirstPlayerController() =>
            GetPlayerController(0);

        private void OnSwitchLevel(SwitchLevelMessage message)
        {
            ReleasePawns();
        }

        private void ReleasePawns()
        {
            foreach (var player in _players)
            {
                var playerCtrl = player.Controller;
                var pawn = playerCtrl.Pawn;

                if (pawn == null)
                {
                    continue;
                }

                playerCtrl.ReleasePawn();
                _objectService.DespawnEntity(pawn);
            }
        }
    }
}