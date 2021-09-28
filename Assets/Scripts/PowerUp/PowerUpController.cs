using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpController : MonoBehaviour
{
    [SerializeField] private PowerUp powerUpItem;
    [SerializeField] private float xAxis,
        zAxis;
    [SerializeField] private bool isPowerUpAvailable;
    
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = PlayerMovement.Instance;
    }

    private void Update()
    {
        if (!isPowerUpAvailable)
        {
            StopAllCoroutines();
            MakePowerUp();
        }

        if (powerUpItem.IsTaken)
        {
            StartCoroutine(SpawnPowerUpDelay(5f));
        }
    }
    
    /// <summary>
    /// Make power up with random x and z axis
    /// </summary>
    private void MakePowerUp()
    {
        isPowerUpAvailable = true;
        powerUpItem.gameObject.SetActive(true);
        // Set position
        xAxis = Random.Range(-20, 20);
        zAxis = Random.Range(-20, 20);
        powerUpItem.transform.position = new Vector3(xAxis, 0.5f, zAxis);
    }
    
    /// <summary>
    /// Give delay to spawn and reset speed
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    private IEnumerator SpawnPowerUpDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        powerUpItem.IsTaken = false;
        playerMovement.ResetSpeed();
        isPowerUpAvailable = false;
    }
}
