using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace DefaultNamespace.Currency
{
    [CreateAssetMenu(menuName = "Currency/CurrencySystem")]
    public class CurrencySystem : ScriptableObject, IInit, ICleanUp
    {
        public List<CurrencyBase> Currencies;
        
        public void Init()
        {
            foreach (var curr in Currencies)
            {
                curr.Load();
            }
        }

        public void CleanUp()
        {
            foreach (var curr in Currencies)
            {
                curr.Save();
            }
        }
    }
}