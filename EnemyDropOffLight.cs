using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyDropOffLight : MonoBehaviour
{
    [SerializeField] private Light2D myLight;

    private float destroyAfter = 0;
    private float destroyTimer = -1f; 
    public void Setup(float destroyTime)
    {
        destroyAfter = destroyTime;
    }

    public void Update()
    {
        destroyTimer += Time.deltaTime;
        if (destroyTimer > destroyAfter)
        {
            Destroy(gameObject);
        }
    }
}
