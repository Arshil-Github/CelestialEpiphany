using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public void Start()
    {
        GameManager.Instance.OnScoreChange += GameManager_OnScoreChange;
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(object sender, GameManager.GameStates e)
    {
        if(e == GameManager.GameStates.GameOver)
        {
            gameObject.SetActive(false);
        }
    }

    private void GameManager_OnScoreChange(object sender, int e)
    {
        scoreText.text = e.ToString();
    }
}
