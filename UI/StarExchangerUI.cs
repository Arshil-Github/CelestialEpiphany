using UnityEngine;
using System;
using System.Collections.Generic;

public class StarExchangerUI : MonoBehaviour
{
    [SerializeField] private StarExchanger starExchanger;
    [SerializeField] private StarExchangerSingleUI pf_StarExchangerSingleUI;
    [SerializeField] private GameObject UIContainer;

    private void Awake()
    {
    }
    private void Start()
    {
        starExchanger.OnShowUI += StarExchanger_OnShowUI;
        starExchanger.OnHideUI += StarExchanger_OnHideUI;
        starExchanger.OnShopReset += StarExchanger_OnShopReset;

        StarExchanger_OnHideUI(this, EventArgs.Empty);

        List<PowerUpEffect> randomPowerUps = starExchanger.GetRandomPowerUps(3);
        foreach (PowerUpEffect effect in randomPowerUps)
        {
            StarExchangerSingleUI spawnedUI = Instantiate(pf_StarExchangerSingleUI, UIContainer.transform);

            spawnedUI.SetUpUI(effect);
        }
    }

    private void StarExchanger_OnShopReset(object sender, EventArgs e)
    {
        foreach(Transform child in UIContainer.transform)
        {
            Destroy(child.gameObject);
        }

        List<PowerUpEffect> randomPowerUps = starExchanger.GetRandomPowerUps(3);
        foreach (PowerUpEffect effect in randomPowerUps)
        {
            StarExchangerSingleUI spawnedUI = Instantiate(pf_StarExchangerSingleUI, UIContainer.transform);

            spawnedUI.SetUpUI(effect);
        }
    }

    private void StarExchanger_OnShowUI(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
    }

    private void StarExchanger_OnHideUI(object sender, System.EventArgs e)
    {
        if(gameObject.activeSelf) gameObject.SetActive(false);
        Debug.Log(gameObject.activeSelf);
    }

}