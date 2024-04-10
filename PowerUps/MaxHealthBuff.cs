using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/MaxHealthBuff")]
public class MaxHealthBuff : PowerUpEffect
{
    [SerializeField] private float amount;
    public override void Apply(Player target)
    {
        target.ChangeMaxHealth(amount);
    }
}
