using UnityEngine;

public abstract class PowerUpEffect : ScriptableObject
{
    public string powerUpName;
    public string description;
    public int cost;
    public int rarity;//1 2 or 3
    public abstract void Apply(Player target);
}