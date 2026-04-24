using UnityEngine;

[CreateAssetMenu(fileName = "WaveConfig", menuName = "New WaveConfig")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] private Transform pathPrefab;
    [SerializeField] private float enemyMoveSpeed = 5f;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float timeBetweenEnemySpawns = 1f;
    [SerializeField] private float enemySpawnVariance = 0f;
    [SerializeField] private float minimumSpawnTime = 0.2f;
    [SerializeField] private float timeBetweenWaves = 0f;

    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }

    public float GetEnemyMoveSpeed()
    {
        return enemyMoveSpeed;
    }

    public Transform[] GetWaypoints()
    {
        Transform[] waypoints = new Transform[pathPrefab.childCount];
        
        // waypoints[0] = pathPrefab.GetChild(0);
        // waypoints[1] = pathPrefab.GetChild(1);

        for (int i = 0; i < pathPrefab.childCount; i++)
        {
            waypoints[i] = pathPrefab.GetChild(i);
        }
        return waypoints;
    }

    public int GetEnemyCount()
    {
        return enemyPrefabs.Length;
    }

    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }

    public float GetRandomEnemySpawnTime()
    {
        float spawnTime = Random.Range(
            timeBetweenEnemySpawns - enemySpawnVariance, 
            timeBetweenEnemySpawns + enemySpawnVariance
            );
        spawnTime = Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);

        return spawnTime;
    }

    public float GetTimeBetweenWaves()
    {
        return timeBetweenWaves;
    }
}
