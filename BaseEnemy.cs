using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseEnemy : MonoBehaviour, IHasHasHealth
{
    public event EventHandler OnHealthChange;

    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float detectionRange;
    [SerializeField] protected float attackRate;
    [SerializeField] protected float attackDamage;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected int killScore = 1;
    [SerializeField] protected Effects pf_dieEffect;
    [SerializeField] protected AudioClip damageAudio;
    [SerializeField] protected float destoryLightsAfter = 1f;

    protected float attackTimer;
    protected float maxAttackTimer;
    protected float currentHealth;
    protected AudioSource audioSource;
    protected bool canMove = false;
    private EnemySpawnerTrap ifSpawnedThroughTrap = null;
    
    //This will spawn using a static location and will chase down the player then when its in attack range it will change state, need a state machine
    private enum State
    {
        Running,
        In_Player_Range,
        Attacking
    }
    private State currentState;

    public void Awake()
    {
        currentHealth = maxHealth;
        maxAttackTimer = 1 / attackRate;

        audioSource = GetComponent<AudioSource>();

        CustomAwake();
    }
    protected virtual void CustomAwake (){ }
    
    public void SpawnedThroughTrap(EnemySpawnerTrap trap) { ifSpawnedThroughTrap=trap; }
    private void Update()
    {

        if (!GameManager.Instance.isGameplayState() || Player.Instance.GetInShadows()) { return; }
        Vector2 distanceDelta = Player.Instance.transform.position - transform.position;
        switch (currentState)
        {
            case State.Running:
                Vector2 moveVector = distanceDelta.normalized * moveSpeed * Time.deltaTime;
                transform.Translate(moveVector);

                if (distanceDelta.magnitude < detectionRange)
                {
                    currentState = State.In_Player_Range;
                }

                break;
            case State.In_Player_Range:

                attackTimer += Time.deltaTime;

                if (attackTimer > maxAttackTimer)
                {
                    Attack();
                    attackTimer = 0;
                }

                //If the distance is greater. switch to running
                if (distanceDelta.magnitude > detectionRange)
                {
                    currentState = State.Running;
                }

                break;
            default:
                break;
        }
    }

    public void ChangeCanMove(bool allowedToMove) { canMove = allowedToMove; }
    //All the attacking logic like which animation to play and how to deal damage.
    public virtual void Attack() {}
    public float GetHealth()
    {
        return currentHealth;
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void ChangeHealth(float amount)
    {
        currentHealth += amount;

        if (currentHealth <= 0)
        {
            HealthOver();
        }
        else
        {
            audioSource.clip = damageAudio;
            audioSource.Play();

            OnHealthChange?.Invoke(this, EventArgs.Empty);
        }
    }

    public void HealthOver()
    {

        Vector3 diePosition = transform.position;
        Destroy(gameObject, 0.1f);

        EffectSpawner.Instance.SpawnEffect(pf_dieEffect, transform.position, () =>
        {
            //GameManager.Instance.AddToScore(killScore);
            ifSpawnedThroughTrap.KilledAnEnemy(this);
            //WaveSpawner.Instance.EnemyKilled(this);
        });
    }
    public void ChangeMaxHealth(float amount) { }
}