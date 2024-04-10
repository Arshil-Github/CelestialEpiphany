using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] SpriteRenderer gunSprite;

    [SerializeField] private GunSO gunSO;
    [SerializeField] private Transform pf_bullet;
    [SerializeField] private AudioClip fireSFX;
    
    private float bulletFireTimer;
    private int bulletFireCount = 0;

    private void Start()
    {
        player.OnGunChange += player_OnGunChange;
    }

    private void player_OnGunChange(object sender, GunSO e)
    {
        gunSO = e;

        //Set up the gun based on the information of the GunSO
        gunSprite.sprite = e.gunSprite;
    }

    private void Update()
    {
        SpriteRotation();

        if(Input.GetMouseButton(0) && GameManager.Instance.isGameplayState())
        {
            ShootBullet();
        }
        bulletFireTimer -= Time.deltaTime;
    }
    private void SpriteRotation()
    {
        //Get the vector between the current transform world positiona nd mousePosition in the world
        Vector3 targetDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        //Calculating the angle between up and targetDIr --> THe weapon must face up 
        float angle = (Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg) - 90f;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void ShootBullet()
    {
        float bulletFireMaxTimer = 1 / gunSO.fireRate;
        Vector3 targetDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;



        if (bulletFireTimer < 0 && bulletFireCount < gunSO.magSize)
        {
            //Actually Spawning the bullet 
            Transform spawnedBullet = Instantiate(pf_bullet, transform.position, Quaternion.LookRotation(targetDir));
            spawnedBullet.GetComponent<Bullet>().SetUp(-gunSO.damage * player.GetAttackMultipier(), targetDir, true, player.GetBulletPiercing());

            bulletFireCount++;
            bulletFireTimer = bulletFireMaxTimer;

            player.PlayPlayerSFX(fireSFX);

            if(bulletFireCount >= gunSO.magSize)
            {
                StartCoroutine("reloadMag");
            }
        }

    }

    private IEnumerator reloadMag()
    {
        Debug.Log("Reloading");
        yield return new WaitForSeconds(gunSO.reloadTime);
        bulletFireCount = 0;
    }

}
