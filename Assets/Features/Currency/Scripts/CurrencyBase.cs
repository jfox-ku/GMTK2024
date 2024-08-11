using System;
using NaughtyAttributes;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace DefaultNamespace.Currency
{
    [Serializable]
    public abstract class CurrencyBase : ScriptableObject
    {
        public string CurrencyName;
        [Expandable]
        public IntVariable Amount;
        [ShowAssetPreview()]
        public Sprite Icon;
        
        public void Add(int amount)
        {
            Amount.Value += amount;
        }

        public bool HasAmount(int amount)
        {
            return Amount.Value >= amount;
        }

        public virtual void Save()
        {
            PlayerPrefs.SetInt(CurrencyName, Amount.Value);
        }
        
        public virtual void Load()
        {
            Amount.Value = PlayerPrefs.GetInt(CurrencyName, 0);
        }
    }
}