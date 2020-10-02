using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Transform player;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private NavMeshAgent nav;

    private void Awake ()
    {
        // Get player
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        
        // Get components
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
    }
    
    private void Update ()
    {
        // Move enemy
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination (player.position);
        }
        else
        {
            nav.enabled = false;
        }
    }
}
