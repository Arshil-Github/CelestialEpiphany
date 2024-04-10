using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDust : MonoBehaviour
{
    float quantity = 0;
    public void Setup(float amount)
    {
        quantity = amount;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player player))
        {
            player.AddStarDust(quantity);
        }
    }
}
