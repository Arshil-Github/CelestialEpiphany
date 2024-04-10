using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : Interactables
{
    public static WaveSpawner Instance;

    public event System.EventHandler<int> OnWaveChanged;

    [SerializeField] private List<BaseEnemy> enemyList;
    [SerializeField] private float linearRate = 1.2f;
    [SerializeField] private int linearTillWave = 10;
    [SerializeField] private Effects pf_spawnEffect;
    [SerializeField] private int spawnSafeAfter = 5;
 
    [SerializeField] private Vector2 spawnRadiusBetween = new Vector2(5, 10);
    [SerializeField] private List<Transform> lightPresets;


    private List<BaseEnemy> spawnedEnemies = new List<BaseEnemy>();
    private Transform spawnedLightPreset;

    private int minimumEnemies = 1;
    private int currentWaveNumber = 1;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
        promptText.SetActive(false);
        gameObject.SetActive(false);
    }

    private void GameManager_OnGameStateChanged(object sender, GameManager.GameStates e)
    {
        if (e != GameManager.GameStates.BetweenWaves)
        {
            gameObject.SetActive(false);
        }
        else
        {
            EffectSpawner.Instance.SpawnStructureEffect(transform.position, () =>
            {
                gameObject.SetActive(true);
            });
        }
    }
    public override void Player_OnInteractPressed(object sender, System.EventArgs e)
    {
        SpawnWave(currentWaveNumber);
    }

    public void SpawnWave(int waveIndex)
    {
        int numberOfEnemies = minimumEnemies + (int)Mathf.Ceil(waveIndex * linearRate);

        for(int i = 0; i < numberOfEnemies; i++)
        {

            Vector2 spawnLocation = new Vector2(Random.Range(spawnRadiusBetween.x, spawnRadiusBetween.y) * GetRandomDirection(), Random.Range(spawnRadiusBetween.x, spawnRadiusBetween.y) * GetRandomDirection());

            EffectSpawner.Instance.SpawnEffect(pf_spawnEffect, spawnLocation, () =>
            {
                int randomIndex = Random.Range(0, enemyList.Count);
                BaseEnemy spawnedEnemy = Instantiate(enemyList[randomIndex]);

                spawnedEnemy.transform.position = spawnLocation;

                spawnedEnemies.Add(spawnedEnemy);
            });
        }

        currentWaveNumber++;
        OnWaveChanged?.Invoke(this, currentWaveNumber);

        //Spawn Lights
        spawnedLightPreset =  Instantiate(lightPresets[Random.Range(0, lightPresets.Count)], Vector3.zero, Quaternion.identity);


        GameManager.Instance.ChangeStateToGamePlay();

    }
    public void EnemyKilled(BaseEnemy enemy)
    {
        spawnedEnemies.Remove(enemy);
   
        if(spawnedEnemies.Count == 0)
        {
            Destroy(spawnedLightPreset.gameObject);
            if (currentWaveNumber % spawnSafeAfter == 0)
            {
                GameManager.Instance.ChangeStateToBetween();
            }
            else
            {
                SpawnWave(currentWaveNumber);
            }
        }
    }
    private int GetRandomDirection()
    {
        return (Random.Range(0, 100) > 50) ? 1 : -1;
    }
    public int GetScore() { return currentWaveNumber; }
}