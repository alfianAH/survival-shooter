using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    private Animator anim;
    private AudioSource playerAudio;
    private PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    private bool isDead,
        damaged;
    
    private void Awake()
    {
        // Get Components
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();

        currentHealth = startingHealth;
    }
    
    private void Update()
    {
        damageImage.color = damaged 
            ? flashColour 
            : Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);

        damaged = false;
    }

    /// <summary>
    /// To get damage
    /// </summary>
    /// <param name="amount"></param>
    public void TakeDamage(int amount)
    {
        damaged = true;
        
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        
        playerAudio.Play();
        
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    /// <summary>
    /// Dead player
    /// </summary>
    private void Death()
    {
        isDead = true;

        playerShooting.DisableEffects();
        
        anim.SetTrigger("Die"); // Play animation
        
        // Play audio
        playerAudio.clip = deathClip;
        playerAudio.Play();
        
        // Disable PlayerMovement script
        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
