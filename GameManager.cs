using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event EventHandler<GameStates> OnGameStateChanged;
    public event EventHandler<int> OnScoreChange;


    private int playerScore = 0;
    
    public enum GameStates
    {
        Starting,
        Gameplay,
        BetweenWaves,
        GameOver
    }
    [SerializeField]private GameStates state;

    private void Awake()
    {
        Instance = this;
        //state = GameStates.Starting;
    }
    public bool isGameplayState()
    {
        return state == GameStates.Gameplay;
    }
    public void ChangeStateToGamePlay() { 
        state = GameStates.Gameplay; 
        OnGameStateChanged?.Invoke(this, GameStates.Gameplay);
    }
    public void ChangeStateToBetween()
    {
        state = GameStates.BetweenWaves;
        OnGameStateChanged?.Invoke(this, GameStates.BetweenWaves);

        StarExchanger.Instance.ResetUsed();
    }

    public void ChangeToGameOver()
    {
        state = GameStates.GameOver;
        OnGameStateChanged?.Invoke(this, GameStates.GameOver);
    }
    public void AddToScore(int score)
    {
        playerScore += score;
        OnScoreChange?.Invoke(this, playerScore);
    }
    public int GetScore() { return playerScore; }
}
