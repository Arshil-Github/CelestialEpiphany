using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightSpawner : MonoBehaviour, IHasHasHealth
{
    [SerializeField] private float timeForSpawn = 2f;
    [SerializeField] private Light2D lightSource;
    [SerializeField] private float maxHealth = 10;
    [SerializeField] private SpriteRenderer squareComponent;

    private float currentHealth;
    private float spawnTimer = -0.1f;

    private void Awake()
    {
        currentHealth = maxHealth;
        ShowLight();
    }
    public void Update()
    {
        spawnTimer -= Time.deltaTime;

        if(spawnTimer <= 0.2f && spawnTimer > 0)
        {
            ShowLight();
        }
    }

    public void ShowLight()
    {
        lightSource.enabled = true;

        foreach(Collider2D collides in Physics2D.OverlapBoxAll(transform.position, squareComponent.transform.localScale, 0f))
        {
            if(collides.TryGetComponent(out BaseEnemy be))
            {
                be.ChangeCanMove(true);
            }
            if (collides.TryGetComponent(out Player p))
            {
                p.ChangeInShadows(false);
            }
        }

    }
    public void HideLight()
    {
        lightSource.enabled = false;

        foreach (Collider2D collides in Physics2D.OverlapBoxAll(transform.position, squareComponent.transform.localScale, 0f))
        {
            if (collides.TryGetComponent(out BaseEnemy be))
            {
                be.ChangeCanMove(false);
            }
            if (collides.TryGetComponent(out Player p))
            {
                p.ChangeInShadows(true);
            }
        }
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public void ChangeHealth(float amount)
    {
        currentHealth += amount;
        
        if(currentHealth <= 0)
        {
            HealthOver();
        }
    }

    public void HealthOver()
    {
        HideLight();
        spawnTimer = timeForSpawn;
    }

    public void ChangeMaxHealth(float amount)
    {
        maxHealth += amount;
    }

}