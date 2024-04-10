using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnerTrap : MonoBehaviour
{

    [SerializeField] private List<GameObject> enemyLocations;
    [SerializeField] private Effects pf_spawnEffect;
    [SerializeField] private List<BaseEnemy> enemyList;
    [SerializeField] private UnityEvent afterKillingEnemies;

    private bool hasSpawned = false;

    private List<BaseEnemy> spawnedEnemies = new List<BaseEnemy>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        if(hasSpawned) { return; }
        int numberOfEnemies = enemyLocations.Count;

        for (int i = 0; i < numberOfEnemies; i++)
        {

            Vector2 spawnLocation = enemyLocations[i].transform.position;

            EffectSpawner.Instance.SpawnEffect(pf_spawnEffect, spawnLocation, () =>
            {
                int randomIndex = Random.Range(0, enemyList.Count);
                BaseEnemy spawnedEnemy = Instantiate(enemyList[randomIndex]);

                spawnedEnemy.transform.position = spawnLocation;

                spawnedEnemy.SpawnedThroughTrap(this);
                spawnedEnemies.Add(spawnedEnemy);
            });
        }

        GameManager.Instance.ChangeStateToGamePlay();

        hasSpawned = true;
    }

    public void KilledAnEnemy(BaseEnemy removedEnemy)
    {
        spawnedEnemies.Remove(removedEnemy);
        if(spawnedEnemies.Count == 0)
        {
            afterKillingEnemies?.Invoke();
            Destroy(gameObject);
        }
    }

}
