using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject[] enemies;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    [SerializeField] private MonoBehaviour factory;
    public IFactory Factory => factory as IFactory;

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
        // int spawnEnemy = Random.Range(0, 3);
        
        // Duplicate enemy
        Factory.FactoryMethod(spawnPointIndex);
    }
}
