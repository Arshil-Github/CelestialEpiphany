using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new Gun", menuName = "GunSO")]
public class GunSO : ScriptableObject
{
    public float damage;
    public float fireRate;

    public Sprite gunSprite;

    public int magSize;
    public float reloadTime;

    public float spreadAngle = 0;
    public int bulletAtOnce = 1;
}
