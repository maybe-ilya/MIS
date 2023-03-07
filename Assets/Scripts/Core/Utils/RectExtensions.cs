using System;
using System.Linq;
using UnityEngine;

namespace mis.Core
{
    public static class RectExtensions
    {
        public static Rect[] SplitHorizontally(this Rect inputRect, int parts)
        {
            if (parts <= 0)
            {
                throw new ArgumentException($"Could not split by {parts} parts");
            }

            if (parts == 1)
            {
                return new[] { inputRect };
            }

            var result = Enumerable.Repeat(inputRect, parts).ToArray();
            var splitWidth = inputRect.width / parts;

            for (var index = 0; index < result.Length; ++index)
            {
                var rect = result[index];
                rect.width = splitWidth;
                rect.x += splitWidth * index;
                result[index] = rect;
            }

            return result;
        }

        public static Rect[] SplitVertically(this Rect inputRect, int parts)
        {
            if (parts <= 0)
            {
                throw new ArgumentException($"Could not split by {parts} parts");
            }

            if (parts == 1)
            {
                return new[] { inputRect };
            }

            var result = Enumerable.Repeat(inputRect, parts).ToArray();
            var splitHeight = inputRect.height / parts;

            for (var index = 0; index < result.Length; ++index)
            {
                var rect = result[index];
                rect.height = splitHeight;
                rect.y += splitHeight * index;
                result[index] = rect;
            }

            return result;
        }

        public static Rect[] CutRight(this Rect inputRect, float cutSize)
        {
            var leftRect = inputRect;
            leftRect.width -= cutSize;

            var rightRect = inputRect;
            rightRect.width = cutSize;
            rightRect.x += leftRect.width;

            return new[] { leftRect, rightRect };
        }

        public static Rect[] CutLeft(this Rect inputRect, float cutSize)
        {
            var rightRect = inputRect;
            rightRect.width -= cutSize;
            rightRect.x += cutSize;

            var leftRect = inputRect;
            leftRect.width = cutSize;

            return new[] { leftRect, rightRect }; ;
        }

        public static Rect[] CutTop(this Rect inputRect, float cutSize)
        {
            var topRect = inputRect;
            topRect.height = cutSize;

            var bottomRect = inputRect;
            bottomRect.height -= cutSize;
            bottomRect.y += cutSize;

            return new[] { topRect, bottomRect };
        }

        public static Rect[] CutBottom(this Rect inputRect, float cutSize)
        {
            var topRect = inputRect;
            topRect.height -= cutSize;

            var bottomRect = inputRect;
            bottomRect.y += topRect.height;
            bottomRect.height = cutSize;

            return new[] { topRect, bottomRect };
        }
    }
}