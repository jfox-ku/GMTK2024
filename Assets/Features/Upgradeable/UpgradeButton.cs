using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Currency;
using DefaultNamespace.Upgradeable;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public Button Button;
    public TextMeshProUGUI CostText;
    public TextMeshProUGUI UpgradeNameText;
    public UpgradeableBase UpgradeBase;
    
    public CurrencyBase Currency;
    public Image CurrencyImage;

    bool CanClick => Currency.HasAmount(Mathf.RoundToInt(UpgradeBase.Cost()));
    private void Start()
    {
        Button.onClick.AddListener(OnButtonClicked);
        Currency.Amount.AddListener(OnCurrencyChanged);
        UpdateView();
    }

    private void OnDestroy()
    {
        Currency.Amount.RemoveListener(OnCurrencyChanged);
    }

    private void OnCurrencyChanged(int arg0)
    {
        UpdateView();
    }

    [Button()]
    void UpdateView()
    {
        Button.interactable = CanClick;
        CostText.text = UpgradeBase.Cost().ToString();
        CurrencyImage.sprite = Currency.Icon;
        UpgradeNameText.text = UpgradeBase.UpgradeName;
    }

    void OnButtonClicked()
    {
        if (!CanClick) return;
        UpgradeBase.Upgrade();
        Currency.Add(-Mathf.RoundToInt(UpgradeBase.Cost()));
        UpdateView();
    }


    private void Reset()
    {
        Button = GetComponent<Button>();
    }
}
