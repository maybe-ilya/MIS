namespace mis.Core
{
    public readonly struct FloatSettingChangedMessage : IMessage
    {
        public readonly GameId SettingId;
        public readonly float OldValue;
        public readonly float NewValue;

        public FloatSettingChangedMessage(GameId settingId, float oldValue, float newValue)
        {
            SettingId = settingId;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }

    public readonly struct BoolSettingChangedMessage : IMessage
    {
        public readonly GameId SettingId;
        public readonly bool OldValue;
        public readonly bool NewValue;

        public BoolSettingChangedMessage(GameId settingId, bool oldValue, bool newValue)
        {
            SettingId = settingId;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}