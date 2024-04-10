using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSwitchUI : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip clipToPlay;

    private void Start()
    {
        SwitchManager.Instance.OnTimeSwitch += SwithcManager_OnTimeSwitch;
    }

    private void SwithcManager_OnTimeSwitch(object sender, System.EventArgs e)
    {
        animator.Play(clipToPlay.name);
    }

}
