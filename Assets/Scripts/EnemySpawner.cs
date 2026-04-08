using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private WaveConfigSO[] waveConfigs;
    // [SerializeField] private WaveConfigSO currentWave;
    [SerializeField] private bool isLooping = true;

    private WaveConfigSO currentWave;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // private void SpawnEnemy()
    // {
    //     for (int i = 0; i< currentWave.GetEnemyCount(); i++)
    //     {    
    //     Instantiate(
    //         currentWave.GetEnemyPrefab(0), 
    //         currentWave.GetStartingWavepoint().position, 
    //         Quaternion.identity,
    //         transform
    //         );
    //     }
    // }

    // private IEnumerator SpawnEnemy()
    // {
    //     foreach (WaveConfigSO wave in waveConfigs)
    //     {
    //         currentWave = wave;
    //         for (int i = 0; i < currentWave.GetEnemyCount(); i++)
    //         {
    //             Instantiate(
    //                 currentWave.GetEnemyPrefab(0),
    //                 currentWave.GetStartingWaypoint().position,
    //                 Quaternion.identity,
    //                 transform
    //                 );
    //             yield return new WaitForSeconds(currentWave.GetRandomEnemySpawnTime());
    //         }
    //         yield return new WaitForSeconds(currentWave.GetTimeBetweenWaves());
    //     }
    // }

    private IEnumerator SpawnEnemy()
    {
        do
        {
            foreach (WaveConfigSO wave in waveConfigs)
            {
                currentWave = wave;
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(
                        currentWave.GetEnemyPrefab(0),
                        currentWave.GetStartingWaypoint().position,
                        Quaternion.identity,
                        transform
                        );
                    yield return new WaitForSeconds(currentWave.GetRandomEnemySpawnTime());
                }
                yield return new WaitForSeconds(currentWave.GetTimeBetweenWaves());
            }
        } while (isLooping);
    }

    public WaveConfigSO GetWaveConfig()
    {
        return currentWave;
    }
}
