using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/HealthRegenBuff")]
public class HealthRegenBuff : PowerUpEffect
{
    [SerializeField] private float amount;
    public override void Apply(Player target)
    {
        target.ChangeHealth(amount);
    }
}
