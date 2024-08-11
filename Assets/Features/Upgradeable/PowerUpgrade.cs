using System;
using UnityEngine;

namespace DefaultNamespace.Upgradeable
{
    [CreateAssetMenu(menuName = "Upgrades/PowerUpgrade")]
    public class PowerUpgrade : UpgradeableBase
    {
        private void Reset()
        {
            UpgradeName = "Power";
        }
    }
}