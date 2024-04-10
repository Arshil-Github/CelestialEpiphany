using System;
using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    public static EffectSpawner Instance { get; private set; }

    [SerializeField] private Effects structureSpawnEffect;

    private void Awake()
    {
        Instance = this;
    }
    public void SpawnEffect(Effects effectToSpawn, Vector2 spawnPosition, Action actionToCall)
    {
        Effects spawnedEffect = Instantiate(effectToSpawn, spawnPosition, Quaternion.identity);
        spawnedEffect.Setup(actionToCall);
    }
    public void SpawnStructureEffect(Vector2 spawnLoc, Action action)
    {
        SpawnEffect(structureSpawnEffect, spawnLoc, action);
    }
}