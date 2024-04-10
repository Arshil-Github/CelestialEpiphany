using System;
using UnityEngine;


public class LevelCheckpoint : Interactables
{
    public override void Player_OnInteractPressed(object sender, EventArgs e)
    {
        SceneSwitcher.Instance.NextLevel();
    }
}