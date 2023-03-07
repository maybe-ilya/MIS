using mis.Core;
using System;
using System.Linq;
using UnityEngine;

namespace mis.Weapons
{
    [CreateAssetMenu(fileName = "WeaponHandlerConfig", menuName = "mis Assets/Weapons/WeaponHandlerConfig")]
    internal sealed class WeaponHandlerConfig : ScriptableObject
    {
        [SerializeField]
        private GameId[] _equippableWeapons;

        public bool IsEquippableWeapon(GameId gameId) =>
            _equippableWeapons.Contains(gameId);
    }
}