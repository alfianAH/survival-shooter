using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    private PlayerHealth playerHealth; // Reference to the player's health.
    private Animator anim; // Reference to the animator component.
    private bool stopPlay;
    private static readonly int GameOver = Animator.StringToHash("GameOver");
    
    private void Awake ()
    {
        // Set up the reference.
        anim = GetComponent <Animator> ();
        playerHealth = PlayerHealth.Instance;
    }
    
    private void Update ()
    {
        // If the player has run out of health...
        if(playerHealth.currentHealth <= 0 && !stopPlay)
        {
            // ... tell the animator the game is over.
            stopPlay = true;
            anim.SetTrigger (GameOver);
        }
    }
}