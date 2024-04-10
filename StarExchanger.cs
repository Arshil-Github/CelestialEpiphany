using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StarExchanger : Interactables
{
    public static StarExchanger Instance;

    public event EventHandler OnShowUI;
    public event EventHandler OnHideUI;
    public event EventHandler OnShopReset;

    [SerializeField] private List<GunSO> gunSOs;
    [SerializeField] private List<PowerUpEffect> powerUpEffectSOs;
    [SerializeField] private Vector2 rarityPercentage;
    [SerializeField] private Color disableColor = Color.red;
    [SerializeField] private SpriteRenderer visual;

    private List<PowerUpEffect> Tier1PowerUps = new List<PowerUpEffect>();
    private List<PowerUpEffect> Tier2PowerUps = new List<PowerUpEffect>();
    private List<PowerUpEffect> Tier3PowerUps = new List<PowerUpEffect>();

    bool isUIShow = false;
    private Color originalColor;

    private void Awake()
    {
        Instance = this;

        originalColor = visual.color;

        foreach(PowerUpEffect powerUpEffect in powerUpEffectSOs)
        {
            if(powerUpEffect.rarity == 1) { Tier1PowerUps.Add(powerUpEffect); }
            else if(powerUpEffect.rarity == 2) { Tier2PowerUps.Add(powerUpEffect); }
            else { Tier3PowerUps.Add(powerUpEffect); }
        }
        promptText.SetActive(false);
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
        gameObject.SetActive(false);
    }

    private void GameManager_OnGameStateChanged(object sender, GameManager.GameStates e)
    {

        gameObject.SetActive(true);
    }

    public override void Player_OnInteractPressed(object sender, System.EventArgs e)
    {
        if (disablePrompt) { return; }
        if (isUIShow)
        {
            OnHideUI?.Invoke(this, EventArgs.Empty);
            isUIShow = false;
        }
        else
        {
            OnShowUI?.Invoke(this, EventArgs.Empty);
            isUIShow = true;
        }
        promptText.SetActive(false);

    }
    public void ExchangeUsed()
    {
        disablePrompt = true;
        visual.color = disableColor;

        visual.GetComponent<Light2D>().enabled = false;
        OnHideUI?.Invoke(this, EventArgs.Empty);

        Player.Instance.OnInteractPressed -= Player_OnInteractPressed;

        isUIShow = false;
    }
    public void ResetUsed() {
        disablePrompt = false;
        visual.color = originalColor;
        OnShopReset?.Invoke(this, EventArgs.Empty);

        visual.GetComponent<Light2D>().enabled = true;

    }

    public List<PowerUpEffect> GetRandomPowerUps(int number)
    {

        List<PowerUpEffect> output = new List<PowerUpEffect>();

        for(int i = 0; i < number; i++)
        {
            int rarityRandom = UnityEngine.Random.Range(0, 100);

            int rarity;
            if (rarityRandom >= (100 - rarityPercentage.x)) { rarity = 3; }
            else if (rarityRandom >= (100 - rarityPercentage.y - rarityPercentage.x)) { rarity = 2; }
            else { rarity = 1; }

            switch (rarity)
            {
                case 1:
                    output.Add(Tier1PowerUps[UnityEngine.Random.Range(0, Tier1PowerUps.Count)]);
                    break;
                case 2:
                    output.Add(Tier2PowerUps[UnityEngine.Random.Range(0, Tier2PowerUps.Count)]);
                    break;
                case 3:
                    output.Add(Tier3PowerUps[UnityEngine.Random.Range(0, Tier3PowerUps.Count)]);
                    break;
            }
        }

        return output;
    }
}