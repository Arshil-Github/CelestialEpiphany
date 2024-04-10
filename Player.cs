using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IHasHasHealth
{
    public event EventHandler<float> OnHealthChange;
    public event EventHandler<float> OnMaxHealthChange;

    public event EventHandler<int> OnSpeedBuffApply;
    public event EventHandler<int> OnAttackBuffApply;
    public event EventHandler<int> OnPiercingBuffApply;

    public static Player Instance;
    public event EventHandler<GunSO> OnGunChange;
    public event EventHandler OnInteractPressed;

    [SerializeField] private float moveSpeed;
    [SerializeField] private GunSO currentGun;
    [SerializeField] private float maxHealth;
    [SerializeField] private float meeleTimeBetween = 1f;
    [SerializeField] private float meeleDamage;
    [SerializeField] private float meeleRadius = 0.5f;
    [SerializeField] private Effects meeleEffect;

    private float attackMultiplier = 1;
    private float speedMultiplier = 1;
    private int bulletPiercing = 1;

    private float currentHealth;
    private float starDust = 0;
    private bool canMeele = true;

    private Vector2 moveVector;
    private AudioSource audioSource;
    private bool inShadows = true;
    private bool restrictMovement = false;
    private void Awake()
    {
        currentHealth = maxHealth;
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Movement();

        if(Input.GetKeyDown(KeyCode.E))
        {
            OnInteractPressed?.Invoke(this, EventArgs.Empty);
        }

        if(Input.GetMouseButtonDown(1) && canMeele && !inShadows)
        {
            MeeleAttack();
            canMeele = false;
        }
    }

    private void MeeleAttack()
    {
        //Play Animation
        //Damage Anything in a certain radius
        restrictMovement = true;
        EffectSpawner.Instance.SpawnEffect(meeleEffect, transform.position, () =>
        {
            foreach (Collider2D collision in Physics2D.OverlapCircleAll(transform.position, meeleRadius))
            {
                if (collision.TryGetComponent(out IHasHasHealth component))
                {
                    if (!(component as Player))
                    {
                        component.ChangeHealth(-meeleDamage);
                    }
                }
            }
            restrictMovement = false;
            StartCoroutine(ChangeCanMeele());
        });

    }
    IEnumerator ChangeCanMeele() { yield return new WaitForSeconds(meeleTimeBetween);  canMeele = true; }
    private void Movement()
    {
        if (restrictMovement) return;
        Vector2 inputVector = new Vector2();

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y += -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x += -1;
        }

        moveVector = inputVector.normalized * moveSpeed * Time.deltaTime * speedMultiplier;

        transform.Translate(moveVector);
    }
    public Vector2 GetMoveVector()
    { return moveVector; }
    public float GetHealth() { return currentHealth; }


    public void ChangeInShadows(bool value) { inShadows = value; }
    public bool GetInShadows() { return inShadows; }


    public void ChangeHealth(float amount)
    {
        currentHealth += amount;

        if(currentHealth < 0)
        {
            HealthOver();
        }

        OnHealthChange?.Invoke(this, currentHealth);
    }

    public void ChangeMaxHealth(float amount)
    {
        float healthPercentage = currentHealth / maxHealth;

        maxHealth += amount;

        currentHealth = healthPercentage * maxHealth;
        //Hi 
        OnHealthChange?.Invoke(this, currentHealth);
        OnMaxHealthChange?.Invoke(this, maxHealth);
    }
    public void HealthOver()
    {
        GameManager.Instance.ChangeToGameOver();
        restrictMovement = true;
    }

    public void BoostMovementSpeed(float amount, int rarity)
    {
        speedMultiplier += amount - 1;
        OnSpeedBuffApply?.Invoke(this, rarity);
    }
    public void BoostAttackDamage(float amount, int rarity)
    {
        attackMultiplier = amount;
        OnAttackBuffApply?.Invoke(this, rarity);
    }
    public void SetBulletPiercing(int amount, int rarity)
    {
        bulletPiercing = amount;
        OnPiercingBuffApply?.Invoke(this, rarity);
    }

    public float GetAttackMultipier() { return attackMultiplier; }
    public int GetBulletPiercing() { return bulletPiercing; }

    public void AddStarDust(float amount) { starDust += amount; }

    public void PlayPlayerSFX(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, meeleRadius);
    }
}
