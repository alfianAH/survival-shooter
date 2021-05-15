using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;

    private Vector3 offset;

    private void Start()
    {
        // Get offset between target and camera
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        // Get camera position
        Vector3 targetCamPos = target.position + offset;
        
        // Set camera position
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
