using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    private Vector3 movement;
    private Animator animator;
    private Rigidbody playerRigidbody;
    
    private int floorMask;
    private float camRayLength = 100f;

    private void Awake()
    {
        // Get mask value from layer "Floor"
        floorMask = LayerMask.GetMask("Floor");
        
        // Get components
        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Get horizontal axis _
        float horizontal = Input.GetAxisRaw("Horizontal");
        
        // Get vertical axis |
        float vertical = Input.GetAxisRaw("Vertical");
        
        Move(horizontal, vertical);
        Turning();
        WalkingAnimation(horizontal, vertical);
    }

    private void WalkingAnimation(float horizontal, float vertical)
    {
        animator.SetBool("IsWalking", horizontal != 0f || vertical != 0f);
    }
    
    /// <summary>
    /// Move to specific position
    /// </summary>
    /// <param name="horizontal"></param>
    /// <param name="vertical"></param>
    private void Move(float horizontal, float vertical)
    {
        movement.Set(horizontal, 0f, vertical);
        
        // Normalized vector to get 0 - 1
        movement = movement.normalized * (speed * Time.deltaTime);
        
        // Move to position
        playerRigidbody.MovePosition(transform.position + movement);
    }
    
    /// <summary>
    /// Turn around
    /// </summary>
    private void Turning()
    {
        // Make ray from mouse position
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(camRay, out var floorHit, camRayLength, floorMask))
        {
            // Get player to mouse position
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            
            // Get new look rotation to hit position
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            
            // Rotate player
            playerRigidbody.MoveRotation(newRotation);
        }
    }
}
