using UnityEngine;

namespace DefaultNamespace.Currency
{
    [CreateAssetMenu(menuName = "Currency/Coin")]
    public class CoinCurrency : CurrencyBase
    {
        private void Reset()
        {
            CurrencyName = "Coin";
        }
    }
}