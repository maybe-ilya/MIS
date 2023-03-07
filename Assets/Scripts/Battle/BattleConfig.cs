using mis.Core;
using UnityEngine;

namespace mis.Battle
{
    [CreateAssetMenu(fileName = "BattleConfig", menuName = "mis Assets/BattleConfig")]
    public class BattleConfig : AbstractConfig, IBattleConfig
    {
        [SerializeField]
        private BattleWaveData[] _waves;

        public IBattleWaveData[] Waves => _waves;

        public int WaveCount => _waves.Length;
    }
}