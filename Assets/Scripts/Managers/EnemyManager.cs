using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    private void Start ()
    {
        // Invoke Spawn every spawnTime
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    private void Spawn ()
    {
        // If player is dead, then don't make new enemy
        if (playerHealth.currentHealth <= 0f) return;
        
        // Get random number
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);
        // Duplicate enemy
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

    }
}
