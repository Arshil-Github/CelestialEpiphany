using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/DamageBuff")]
public class DamageBuff : PowerUpEffect
{
    [SerializeField] private float m_Damage;
    public override void Apply(Player target)
    {
        target.BoostAttackDamage(m_Damage, rarity);
    }
}
