namespace mis.Core
{
    public interface IRange<T>
    {
        T Min { get; }
        T Max { get; }
        T Clamp(T value);
        bool IsInside(T value);
        bool IsOutside(T value);
        T Lerp(float step);
    }
}