using mis.Core;
using System;
using UnityEngine;

namespace mis.Drop
{
    [Serializable]
    public sealed class DropData : IDropData
    {
        [SerializeField]
        private Resource[] _resources;

        public Resource[] Resources => _resources;
    }
}