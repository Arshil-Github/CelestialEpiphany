using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarExchangerSingleUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI rarityText;
    [SerializeField] private Button purchaseButton;
    [SerializeField] private Image rarityImage;
    [SerializeField] private Color commonColor;
    [SerializeField] private Color epicColor;
    [SerializeField] private Color legendaryColor;

    public void SetUpUI(PowerUpEffect powerUpEffect)
    {
        nameText.text = powerUpEffect.powerUpName;
        descriptionText.text = powerUpEffect.description;
        costText.text = powerUpEffect.cost.ToString();

        string rarityString;
        if (powerUpEffect.rarity == 1) { rarityString = "Common"; rarityImage.color = commonColor; }
        else if(powerUpEffect.rarity == 2) { rarityString = "Epic"; rarityImage.color = epicColor; }
        else if (powerUpEffect.rarity == 3) { rarityString = "Legendary"; rarityImage.color = legendaryColor; }
        else { rarityString = "Unknown"; }

        rarityText.text = rarityString;

        purchaseButton.onClick.AddListener(() =>
        {
            powerUpEffect.Apply(Player.Instance);
            StarExchanger.Instance.ExchangeUsed();
        });

    }
}
