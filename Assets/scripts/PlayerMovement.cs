using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Reference to the CharacterController component
    public CharacterController controller;

    // Movement speed
    public float speed = 12f;

    // Gravity force applied to the player
    public float gravity = -9.81f * 2;

    // Jump height
    public float jumpHeight = 3f;

    // Ground check parameters
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    // Player's vertical velocity
    Vector3 velocity;

    // Flag to check if the player is grounded
    bool isGrounded;



    private Vector3 lastPosition = new Vector3(0, 0, 0);
    public bool isMoving = false;

    // Update is called once per frame
    void Update()
    {
        // Checking if the player hit the ground to reset falling velocity
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Get input for movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 move = transform.right * x + transform.forward * z;

        // Move the player using CharacterController
        controller.Move(move * speed * Time.deltaTime);

        // Check if the player is on the ground to allow jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Equation for jumping
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity to the player's vertical velocity
        velocity.y += gravity * Time.deltaTime;

        // Move the player vertically using CharacterController
        controller.Move(velocity * Time.deltaTime);
        if (lastPosition != gameObject.transform.position && isGrounded==true)
        {
            isMoving = true;
            Sound_Manager.Instance.PlaySound(Sound_Manager.Instance.walkSound);
        }
        else
        {
            isMoving = false;
            Sound_Manager.Instance.walkSound.Stop();
        }
        lastPosition = gameObject.transform.position;
    }
}
