// Importing necessary libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define a class named GoblinController
public class GoblinController : MonoBehaviour
{
    // Public variables accessible in the Unity Editor
    public float speed; // Speed at which the goblin moves
    public float chaseRange; // Define the range at which the Goblin starts chasing the player
    public float groundDist; // Distance from the ground for raycasting
    public float wanderRange; // Range for random wandering

    // References to components in the Unity Editor
    public LayerMask terrainLayer; // Layer mask for detecting terrain
    public Rigidbody rb; // Reference to the Rigidbody component
    public SpriteRenderer sr; // Reference to the SpriteRenderer component
    public Animator animator; // Reference to the Animator component

    public Transform player; // Reference to the Player

    // Private variables
    private Vector3 initialPosition; // Initial position of the goblin
    private Vector3 targetPosition; // Target position for movement
    private bool isMoving = false; // Flag to indicate if goblin is currently moving

    // Start is called before the first frame update
    void Start()
    {
        // Get references to components
        rb = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // Store the initial position of the goblin
        initialPosition = transform.position;

        // Start the movement routine
        StartCoroutine(MoveRoutine());
    }

    // Coroutine for goblin movement
    IEnumerator MoveRoutine()
    {
        while (true)
        {
            // Generate random move and wait durations
            float moveDuration = Random.Range(1f, 3f);
            yield return MoveForDuration(moveDuration);

            float waitDuration = Random.Range(5f, 10f);
            yield return new WaitForSeconds(waitDuration);
        }
    }

    // Coroutine for moving the goblin for a specified duration
    IEnumerator MoveForDuration(float duration)
    {
        float endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            // Perform raycast to determine ground position
            RaycastHit hit;
            Vector3 castPos = transform.position;
            castPos.y += 1;

            if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }

            // Check if player is within chase range
            if (player != null && Vector3.Distance(transform.position, player.position) <= chaseRange)
            {
                ChasePlayer();
            }
            else
            {
                MoveToTarget();
                CheckFlip();
            }

            yield return null;
        }

        // Reset velocity and animation state
        rb.velocity = Vector3.zero;
        isMoving = false;
        animator.SetBool("IsWalking", false);
    }

    // Function to make the goblin chase the player
    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;
        isMoving = true;
        animator.SetBool("IsWalking", true);

        // Flip the sprite based on movement direction
        sr.flipX = direction.x < 0;
    }

    // Function to move the goblin towards the target position
    void MoveToTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        rb.velocity = direction * speed;

        // Check if goblin has reached target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            PickNewTargetPosition();
        }
        else
        {
            isMoving = true;
        }

        // Update animation state
        animator.SetBool("IsWalking", isMoving);
    }

    // Function to pick a new random target position for wandering
    void PickNewTargetPosition()
    {
        float randomX = Random.Range(-wanderRange, wanderRange);
        float randomZ = Random.Range(-wanderRange, wanderRange);

        targetPosition = initialPosition + new Vector3(randomX, 0, randomZ);

        // Also randomly flip the sprite
        sr.flipX = randomX < 0;
    }

    // Function to check if the goblin needs to be flipped based on movement direction
    void CheckFlip()
    {
        if (rb.velocity.x != 0)
        {
            sr.flipX = rb.velocity.x < 0;
        }
    }
}
