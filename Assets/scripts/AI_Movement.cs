using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Movement : MonoBehaviour
{
    // Variables for animator, movement speed, stop position, and timers
    Animator animator;
    public float moveSpeed = 0.2f;
    Vector3 stopPosition;
    float walkTime;
    public float walkCounter;
    float waitTime;
    public float waitCounter;

    // Variables for walk direction and movement status
    int WalkDirection;
    public bool isWalking;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize animator and set random walk and wait times
        animator = GetComponent<Animator>();
        walkTime = Random.Range(3, 6);
        waitTime = Random.Range(5, 7);
        waitCounter = waitTime;
        walkCounter = walkTime;

        // Start walking in a random direction
        ChooseDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            // Set animator parameter for running
            animator.SetBool("isRunning", true);

            // Decrease walk timer
            walkCounter -= Time.deltaTime;

            // Move the AI based on the chosen direction
            switch (WalkDirection)
            {
                case 0:
                    // Move forward
                    transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
                case 1:
                    // Move right
                    transform.localRotation = Quaternion.Euler(0f, 90, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
                case 2:
                    // Move left
                    transform.localRotation = Quaternion.Euler(0f, -90, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
                case 3:
                    // Move backward
                    transform.localRotation = Quaternion.Euler(0f, 180, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
            }

            // Check if the walk timer is depleted
            if (walkCounter <= 0)
            {
                // Stop walking, store current position, reset variables
                stopPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                isWalking = false;
                transform.position = stopPosition;
                animator.SetBool("isRunning", false);
                waitCounter = waitTime; // reset wait timer
            }
        }
        else
        {
            // Decrease wait timer
            waitCounter -= Time.deltaTime;

            // Check if the wait timer is depleted
            if (waitCounter <= 0)
            {
                // Choose a new direction to walk in
                ChooseDirection();
            }
        }
    }

    // Function to randomly choose a walking direction
    public void ChooseDirection()
    {
        WalkDirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    }
}
