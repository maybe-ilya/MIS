namespace mis.Core
{
    public interface IGameSettingsService : IService
    {
        float GetFloatSettingValue(GameId settingId);
        FloatRange GetFloatSettingLimit(GameId settingId);
        float SetFloatSettingValue(GameId settingId, float value);
        bool GetBoolSettingValue(GameId settingId);
        void SetBoolSettingValue(GameId settingId, bool value);
    }
}