using UnityEngine;

public interface IHasHasHealth
{
    public float GetHealth();
    public void ChangeHealth(float amount);
    public void HealthOver() ;

    public void ChangeMaxHealth(float amount);
}

