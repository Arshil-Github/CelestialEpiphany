using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    public static SwitchManager Instance;

    public event EventHandler OnTimeSwitch;

    [SerializeField] private GameObject pastLevel;
    [SerializeField] private GameObject futureLevel;

    [SerializeField] private float timeBetweenSwitchMax;

    private bool inPast = false;
    private float switchTimer = 0f;
    private bool canSwitch = true;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        SwitchTimes(() => { }, () => { });
    }
    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.Space) && canSwitch)
        {
            SwitchTimes(() => { }, () => { });
            canSwitch = false;
        } */   

        if(!canSwitch)
        {
            switchTimer += Time.deltaTime;
            if(switchTimer > timeBetweenSwitchMax )
            {
                canSwitch = true;
                switchTimer = 0f;
            }
        }
    }
    public void SwitchTimes(Action beforeSwitch, Action afterSwitch)
    {
        beforeSwitch?.Invoke();
        inPast = !inPast;
        if (inPast)
        {
            pastLevel.SetActive(true);
            futureLevel.SetActive(false);
        }
        else
        {
            pastLevel.SetActive(false);
            futureLevel.SetActive(true);
        }

        OnTimeSwitch?.Invoke(this, EventArgs.Empty);
        afterSwitch?.Invoke();
    }
}
