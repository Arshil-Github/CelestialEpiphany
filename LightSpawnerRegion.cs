using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpawnerRegion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player p))
        {
            p.ChangeInShadows(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player p))
        {
            p.ChangeInShadows(true);
        }
    }
}
