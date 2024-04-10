using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/PiercingBuff")]
public class PiercingBuff : PowerUpEffect
{
    [SerializeField] private int amount;
    public override void Apply(Player target)
    {
        target.SetBulletPiercing(amount, rarity);
    }
}
