using UnityEngine;

public class PathFinding : MonoBehaviour
{
    private EnemySpawner enemySpawner;
    private WaveConfigSO waveConfig;
    private Transform[] waypoints;
    private int wayPointIndex = 0;

    void Start()
    {
        enemySpawner = FindFirstObjectByType<EnemySpawner>();
        waveConfig = enemySpawner.GetWaveConfig();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waveConfig.GetStartingWaypoint().position;
    }

    void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if (wayPointIndex < waypoints.Length)
        {
            Vector3 targetPosition = waypoints[wayPointIndex].position;
            float moveDelta = waveConfig.GetEnemyMoveSpeed() * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveDelta);
            if (transform.position == targetPosition)
                wayPointIndex++;
        }
        else
            Destroy(gameObject);
    }
}
