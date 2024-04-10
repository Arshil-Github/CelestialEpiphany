using System;
using UnityEditor;
using UnityEngine;

public class Shooter : BaseEnemy
{
    [SerializeField] private Transform pf_bullet;
    [SerializeField] private float speedOfBullet = 5;
    public override void Attack()
    {
        if (Player.Instance.GetInShadows()) return;
        Vector3 targetDir = Player.Instance.transform.position - transform.position;


        Transform spawnedBullet = Instantiate(pf_bullet, transform.position, Quaternion.identity);
        spawnedBullet.GetComponent<Bullet>().SetUp(-attackDamage, targetDir, false, speedOfBullet);
    }
}