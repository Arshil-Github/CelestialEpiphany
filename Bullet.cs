using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float maxTime;


    private float hitRadius = 0.5f;

    private float destroyTimer;

    private float damage;
    private Rigidbody2D rb;
    private bool isPlayerFriendly = true;
    private float bullet_Piercing;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void SetUp(float dmg, Vector2 moveDir, bool playerFriendly, float speed = 0, float piercing = 1)
    {
        damage = dmg;
        isPlayerFriendly = playerFriendly;

        bulletSpeed = (speed == 0) ? bulletSpeed : speed;
        bullet_Piercing = piercing;

        rb.AddForce(moveDir.normalized * bulletSpeed * 10, ForceMode2D.Impulse);
    }

    public void Update()
    {
        destroyTimer += Time.deltaTime;
        if(destroyTimer > maxTime)
        {
            Destroy(gameObject);
        }

        //Collision Detection Logic
        foreach(Collider2D c in Physics2D.OverlapCircleAll(transform.position, hitRadius))
        {
            if(c.TryGetComponent(out BaseEnemy enemyComponent) && isPlayerFriendly)
            {
                ImpartDamage(enemyComponent);
            }
            if (c.TryGetComponent(out Player playerComponent) && !isPlayerFriendly)
            {
                ImpartDamage(playerComponent);
            }
            if(c.TryGetComponent(out LightSpawner lightComponent))
            {
                ImpartDamage(lightComponent);
            }
        }

    }

    private void ImpartDamage(IHasHasHealth target)
    {
        target.ChangeHealth(damage);
        bullet_Piercing -= 1;

        if(bullet_Piercing <= 0)
        {
            Destroy(gameObject);
        }
    }
}
