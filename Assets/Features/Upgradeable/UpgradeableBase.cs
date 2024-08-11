using DefaultNamespace.Currency;
using NaughtyAttributes;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace DefaultNamespace.Upgradeable
{
    public abstract class UpgradeableBase : ScriptableObject
    {
        public string UpgradeName;
        public string Description;
        public Sprite Icon;
        public IntVariable Level;
        public int MaxLevel;

        public bool UseCostCurve;
        [ShowIf("UseCostCurve")]
        public AnimationCurve CostCurve;
        
        public bool UseValueMappingCurve;
        [ShowIf("UseValueMappingCurve")]
        public AnimationCurve ValueCurve;

        public bool CanUpgrade()
        {
            return Level.Value < MaxLevel;
        }

        public void Upgrade()
        {
            Level.Value++;
        }
        
        public float Cost()
        {
            if (UseCostCurve)
            {
                return CostCurve.Evaluate(Level.Value);
            }
            return Level.Value;
        }

        public float Value()
        {
            if (UseValueMappingCurve)
            {
                return ValueCurve.Evaluate(Level.Value);
            }
            
            return Level.Value;
        }
        
        public virtual void Save()
        {
            PlayerPrefs.SetInt(UpgradeName, Level.Value);
        }
        
        public virtual void Load()
        {
            Level.Value = PlayerPrefs.GetInt(UpgradeName, 0);
        }
    }
}