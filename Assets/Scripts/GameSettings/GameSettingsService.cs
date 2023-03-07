using mis.Core;
using System;
using System.IO;
using UnityEngine;

namespace mis.GameSettings
{
    public sealed class GameSettingsService : IGameSettingsService, IStartableService
    {
        private const string SAVE_FILE_NAME = "Settings/GameSettings.json";

        private readonly IMessageService _messageService;
        private readonly IConfigService _configsService;
        private readonly IFilesService _filesService;
        private GameSettingsConfig _config;
        private GameSettingsData _gameSettingsData;

        public int StartPriority => int.MinValue + 1;

        public GameSettingsService(
            IMessageService messageService,
            IConfigService configService,
            IFilesService filesService)
        {
            _messageService = messageService;
            _configsService = configService;
            _filesService = filesService;
        }

        void IStartableService.OnServiceStart()
        {
            _config = _configsService.GetConfig<GameSettingsConfig>(GameIds.GLOBAL_GAME_SETTINGS_CFG);
            InitializeGameSettingsData();
        }

        public float GetFloatSettingValue(GameId settingId) =>
            _gameSettingsData.GetFloatSettingValue(settingId);

        public FloatRange GetFloatSettingLimit(GameId settingId) =>
            _config.GetFloatSettingLimit(settingId).Range;

        public float SetFloatSettingValue(GameId settingId, float value)
        {
            var oldValue = _gameSettingsData.GetFloatSettingValue(settingId);
            var limit = GetFloatSettingLimit(settingId);
            var newValue = limit.Clamp(value);
            newValue = (float)Math.Round(newValue, 1);

            _gameSettingsData.SetFloatSettingValue(settingId, newValue);

            SendMessage(new FloatSettingChangedMessage(settingId, oldValue, newValue));
            SaveData();

            return newValue;
        }

        public bool GetBoolSettingValue(GameId settingId) =>
            _gameSettingsData.GetBoolSettingValue(settingId);

        public void SetBoolSettingValue(GameId settingId, bool value)
        {
            var oldValue = GetBoolSettingValue(settingId);
            _gameSettingsData.SetBoolSettingValue(settingId, value);
            SendMessage(new BoolSettingChangedMessage(settingId, oldValue, value));
            SaveData();
        }

        private void SendMessage(IMessage message) =>
            _messageService.Send(message);

        private string GetFullSettingFilePath() =>
            Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);

        private void InitializeGameSettingsData()
        {
            var path = GetFullSettingFilePath();
            if (_filesService.IsFileExists(path))
            {
                var text = _filesService.ReadTextFile(path);
                _gameSettingsData = JsonUtility.FromJson<GameSettingsData>(text);
            }
            else
            {
                _gameSettingsData = new GameSettingsData(_config.InitialSettings);
                SaveData();
            }
        }

        private void SaveData()
        {
            var path = GetFullSettingFilePath();
            _filesService.CreateDirectorySave(Path.GetDirectoryName(path));
            var text = JsonUtility.ToJson(_gameSettingsData, true);
            _filesService.WriteTextFile(path, text);
        }
    }
}