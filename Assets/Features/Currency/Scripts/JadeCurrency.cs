using System;
using UnityEngine;

namespace DefaultNamespace.Currency
{
    [CreateAssetMenu(menuName = "Currency/Jade")]
    public class JadeCurrency : CurrencyBase
    {
        private void Reset()
        {
            CurrencyName = "Jade";
        }
    }
}