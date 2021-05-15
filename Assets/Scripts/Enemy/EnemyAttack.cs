using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;
    
    private Animator anim;
    private GameObject player;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private bool playerInRange;
    private float timer;
    private static readonly int PlayerDead = Animator.StringToHash("PlayerDead");

    private void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        
        // Get components
        playerHealth = PlayerHealth.Instance;
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }
    
    private void Update ()
    {
        timer += Time.deltaTime;
        
        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack ();
        }
        
        // Trigger animation if player's health is less or equals to zero 
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger(PlayerDead);
        }
    }
    
    private void OnTriggerEnter (Collider other)
    {
        // If player enter the collider, ...
        if(other.gameObject == player && other.isTrigger == false)
        {
            playerInRange = true;
        }
    }
    
    private void OnTriggerExit (Collider other)
    {
        // If player exit collider, ...
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }
    
    private void Attack ()
    {
        timer = 0f; // Reset timer
        
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);
        }
    }
}
