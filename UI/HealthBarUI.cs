using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image healthBarFillImage;
    [SerializeField] private Image healthBarFillBackground;
    [SerializeField] private Image healthBarBorder;

    private float maxHealth;

    private void Start()
    {
        maxHealth = Player.Instance.GetHealth();
        Player.Instance.OnHealthChange += Player_OnHealthChange;
        Player.Instance.OnMaxHealthChange += Player_OnMaxHealthChange;
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(object sender, GameManager.GameStates e)
    {
        if (e == GameManager.GameStates.GameOver)
        {
            gameObject.SetActive(false);
        }
    }

    private void Player_OnMaxHealthChange(object sender, float e)
    {
        //Change Scale

        Vector3 localScale = healthBarFillBackground.rectTransform.localScale;
        float newX = 1 + maxHealth / e;

        Vector3 newScale = new Vector3(newX, localScale.y, localScale.z);

        healthBarFillBackground.rectTransform.localScale = newScale;
        healthBarFillImage.rectTransform.localScale = newScale;
        healthBarBorder.rectTransform.localScale = newScale;

        maxHealth = e;


        healthBarFillImage.fillAmount = e / maxHealth;
    }

    private void Player_OnHealthChange(object sender, float e)
    {
        healthBarFillImage.fillAmount = e / maxHealth;
    }
}