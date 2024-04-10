using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private Button restartButton;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += Instance_OnGameStateChanged;
        Hide();
    }

    private void Instance_OnGameStateChanged(object sender, GameManager.GameStates e)
    {
        if(e == GameManager.GameStates.GameOver)
        {
            Show();
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
        scoreText.text = GameManager.Instance.GetScore().ToString();
        //waveText.text = WaveSpawner.Instance.GetScore().ToString();

        restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}