using System;
using UnityEngine;

public class LevelSwitcherObject : Interactables
{
    public override void Player_OnInteractPressed(object sender, EventArgs e)
    {
        SwitchManager.Instance.SwitchTimes(() => { }, () => { });
    }
}