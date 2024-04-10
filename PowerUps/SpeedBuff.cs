using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/SpeedBuff")]
public class SpeedBuff : PowerUpEffect
{
    [SerializeField] private float m_Speed;
    public override void Apply(Player target)
    {
        target.BoostMovementSpeed(m_Speed, rarity);
    }
}
