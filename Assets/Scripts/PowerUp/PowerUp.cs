using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private int additionalHealth = 30,
        additionalSpeed = 3;

    private bool isTaken;

    public bool IsTaken
    {
        get => isTaken;
        set => isTaken = value;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Check if other is player and its collider is not the trigger one
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            isTaken = true;
            PlayerHealth.Instance.AddHealth(additionalHealth);
            PlayerMovement.Instance.speed += additionalSpeed;
            gameObject.SetActive(false);
        }
    }
}
