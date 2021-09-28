using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : SingletonBaseClass<PlayerHealth>
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage,
        fillHealthSlider;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    
    private Animator anim;
    private AudioSource playerAudio;
    private PlayerMovement playerMovement;
    private PlayerShooting playerShooting;
    private bool isDead,
        damaged;

    private static readonly int Die = Animator.StringToHash("Die");

    private void Awake()
    {
        // Get Components
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = PlayerMovement.Instance;
        playerShooting = GetComponentInChildren<PlayerShooting>();

        currentHealth = startingHealth;
        ChangeHealthColor();
    }
    
    private void Update()
    {
        damageImage.color = damaged 
            ? flashColour 
            : Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);

        damaged = false;
    }
    
    /// <summary>
    /// Change Health color
    /// </summary>
    private void ChangeHealthColor()
    {
        if (currentHealth > 70)
        {
            fillHealthSlider.color = Color.green;
        }
        else if (currentHealth > 30 && currentHealth <= 70)
        {
            fillHealthSlider.color = Color.yellow;
        }
        else
        {
            fillHealthSlider.color = Color.red;
        }
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
        ChangeHealthColor();
        playerAudio.Play();
        
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }
    
    /// <summary>
    /// Add Health
    /// </summary>
    /// <param name="amount"></param>
    public void AddHealth(int amount)
    {
        if(currentHealth > 0 && !isDead)
        {
            currentHealth += amount;
            healthSlider.value = currentHealth;
            ChangeHealthColor();
        }
    }

    /// <summary>
    /// Dead player
    /// </summary>
    private void Death()
    {
        isDead = true;

        playerShooting.DisableEffects();
        
        anim.SetTrigger(Die); // Play animation
        
        // Play audio
        playerAudio.clip = deathClip;
        playerAudio.Play();
        
        // Disable PlayerMovement script
        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }
}
