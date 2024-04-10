using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectsDisplay : MonoBehaviour
{
    [SerializeField] private TrailRenderer speedBuffTrail;
    [SerializeField] private Transform attackHalo;
    [SerializeField] private Transform piercingDamageHalo;

    private void Awake()
    {
            piercingDamageHalo.gameObject.SetActive(false);
            attackHalo.gameObject.SetActive(false);
            speedBuffTrail.gameObject.SetActive(false);
    }

    private void Start()
    {
        Player.Instance.OnSpeedBuffApply += Player_OnSpeedBuffApply;
        Player.Instance.OnAttackBuffApply += Player_OnAttackBuffApply;
        Player.Instance.OnPiercingBuffApply += Player_OnPiercingBuffApply;
    }

    private void Player_OnPiercingBuffApply(object sender, int e)
    {
        piercingDamageHalo.gameObject.SetActive(true);
    }

    private void Player_OnAttackBuffApply(object sender, int e)
    {
        attackHalo.gameObject.SetActive(true);
    }

    private void Player_OnSpeedBuffApply(object sender, int e)
    {
        speedBuffTrail.gameObject.SetActive(true);
    }

}
