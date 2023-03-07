namespace mis.Core
{
    public interface IWidget : IGameEntityComponent
    {
        WidgetState State { get; }
        void Open();
        void Show();
        void Hide();
        void Close();
    }
}