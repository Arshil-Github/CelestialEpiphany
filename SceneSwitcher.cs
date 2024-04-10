using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public static SceneSwitcher Instance;
    [SerializeField] private List<string> LevelsInOrderIndex;
    [SerializeField] private string MainMenuScene;
    [SerializeField] private string GameOverScene;

    private int currentLevel = -1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void NextLevel()
    {
        currentLevel++;
        if(currentLevel >= LevelsInOrderIndex.Count)
        {
            Debug.Log(currentLevel);
            //Exhausted All the Levels
            SceneManager.LoadScene(GameOverScene);
        }
        else
        {
            SceneManager.LoadScene(LevelsInOrderIndex[currentLevel]);
        }

    }
}
