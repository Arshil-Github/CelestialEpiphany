using System;
using UnityEngine;

public class GameStarter : Interactables
{
    public override void Player_OnInteractPressed(object sender, EventArgs e)
    {
        GameManager.Instance.ChangeStateToGamePlay();
        WaveSpawner.Instance.SpawnWave(1);
        gameObject.SetActive(false);
    }
}