using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveNumberUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveNumberText;

    private void Start()
    {
        WaveSpawner.Instance.OnWaveChanged += WaveSpawner_OnWaveChanged;
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void WaveSpawner_OnWaveChanged(object sender, int e)
    {
        waveNumberText.text = e.ToString();
    }
    private void GameManager_OnGameStateChanged(object sender, GameManager.GameStates e)
    {
        if (e == GameManager.GameStates.GameOver)
        {
            gameObject.SetActive(false);
        }
    }
}
