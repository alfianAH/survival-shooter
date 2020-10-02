using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
    
    private Animator anim;
    private AudioSource enemyAudio;
    private ParticleSystem hitParticles;
    private CapsuleCollider capsuleCollider;
    private bool isDead,
        isSinking;
    
    private void Awake ()
    {
        // Get components
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth; // Set current health
    }
    
    private void Update ()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * (sinkSpeed * Time.deltaTime));
        }
    }
    
    /// <summary>
    /// Enemy take damage
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="hitPoint"></param>
    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        // If enemy is dead, return
        if (isDead) return;
        
        enemyAudio.Play (); // Play audio
        
        currentHealth -= amount; // Subtract health
        
        // Play particle
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();
        
        // If current health is less than or equals to 0, then death
        if (currentHealth <= 0)
        {
            Death ();
        }
    }
    
    /// <summary>
    /// Enemy is dead
    /// </summary>
    private void Death ()
    {
        isDead = true;
        
        capsuleCollider.isTrigger = true;
        
        anim.SetTrigger ("Dead");
        
        // Play death audio
        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }
    
    /// <summary>
    /// Will be called when animation event is dead animation
    /// </summary>
    public void StartSinking ()
    {
        // Disable nav mesh component
        GetComponent<NavMeshAgent>().enabled = false;
        // Set rigidbody to kinematic
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }
}
