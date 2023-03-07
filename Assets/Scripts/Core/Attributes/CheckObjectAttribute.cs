using System;
using UnityEngine;

namespace mis.Core
{
    public enum CheckHighlight
    {
        Color = 0,
        Background = 1,
        Content = 2,
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public sealed class CheckObjectAttribute : PropertyAttribute
    {
        public readonly CheckHighlight Highlight;

        public CheckObjectAttribute(CheckHighlight highlight = CheckHighlight.Color)
        {
            Highlight = highlight;
        }
    }
}