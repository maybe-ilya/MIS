using URandom = UnityEngine.Random;

namespace mis.Core
{
    public static class RandomUtils
    {
        public static bool GetRandomBool() =>
            URandom.Range(0, 2) == 1;

        public static float GetRandonValue(this IRange<float> range) =>
            URandom.Range(range.Min, range.Max);
    }
}