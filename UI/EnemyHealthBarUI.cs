using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarUI : MonoBehaviour
{
    [SerializeField] private BaseEnemy enemy;
    [SerializeField] private Image healthImage;

    private void Awake()
    {
        enemy.OnHealthChange += Enemy_OnHealthChange;
    }

    private void Enemy_OnHealthChange(object sender, System.EventArgs e)
    {
        healthImage.fillAmount = enemy.GetHealth() / enemy.GetMaxHealth();
    }
}
