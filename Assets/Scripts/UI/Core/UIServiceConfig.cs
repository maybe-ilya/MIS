using mis.Core;
using UnityEngine;

namespace mis.UI
{
    [CreateAssetMenu(fileName = "UIServiceConfig", menuName = "mis Assets/UIServiceConfig")]
    internal sealed class UIServiceConfig : AbstractConfig, IUIServiceConfig
    {
        [SerializeField]
        [CheckObject]
        private GameObject _uiEventSystemPrefab;

        [SerializeField]
        [CheckObject]
        private GameObject _uiCanvasPrefab;

        public GameObject UIEventSystemPrefab => _uiEventSystemPrefab;

        public GameObject UICanvasPrefab => _uiCanvasPrefab;
    }
}