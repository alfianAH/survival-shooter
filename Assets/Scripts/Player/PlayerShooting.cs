using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;                  
    public float timeBetweenBullets = 0.15f;        
    public float range = 100f;                      
    
    private Ray shootRay;
    private RaycastHit shootHit;
    private ParticleSystem gunParticles;
    private LineRenderer gunLine;
    private AudioSource gunAudio;
    private Light gunLight;
    private float effectsDisplayTime = 0.2f,
        timer;
    private int shootableMask;

    private void Awake()
    {
        // Get mask
        shootableMask = LayerMask.GetMask("Shootable");
        
        // Get components
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets)
        {
            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }
    
    /// <summary>
    /// Disable effects
    /// </summary>
    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
    
    /// <summary>
    /// Shoot enemy
    /// </summary>
    private void Shoot()
    {
        timer = 0f;
        
        gunAudio.Play(); // Play gun audio
        gunLight.enabled = true; // Enable light
        
        // Play gun particle
        gunParticles.Stop();
        gunParticles.Play();
        
        // Enable gun line and set position
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);
        
        // Set ray shoot position and direction
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            
            // If it hits enemy, ...
            if (enemyHealth != null)
            {
                // Enemy take damage
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }
            // Set line end position to hit position
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            // Set line end position to range from barrel
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}