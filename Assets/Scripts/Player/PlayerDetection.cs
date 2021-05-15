using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public GameObject warningText;
    private PlayerHealth playerHealth;
    private WarningManager warningManager;
    
    private void Awake()
    {
        playerHealth = PlayerHealth.Instance;
        warningManager = WarningManager.Instance;
    }

    private void OnTriggerStay(Collider other)
    {
        if (playerHealth.currentHealth <= 0)
        {
            warningText.gameObject.SetActive(false);
            return;
        }
        
        if (other.CompareTag("Enemy") && !other.isTrigger)
        {
            float enemyDistance = Vector3.Distance(transform.position, other.transform.position);
            warningManager.ShowWarning(enemyDistance);
        }
    }
}
