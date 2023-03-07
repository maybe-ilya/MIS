using UnityEngine;

namespace mis.Core
{
    internal sealed class PersistentObject : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
