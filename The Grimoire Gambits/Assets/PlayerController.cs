using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkingSpeed = 5f; // Speed at which the player walks
    public float runningSpeed = 10f; // Speed at which the player runs
    public float groundDist; // Distance from the ground for raycasting
    public LayerMask terrainLayer; // Layer mask for detecting terrain
    public Rigidbody rb; // Reference to the Rigidbody component
    public SpriteRenderer sr; // Reference to the SpriteRenderer component
    public Animator animator; // Reference to the Animator component

    void Start()
    {
        // Get references to components
        rb = gameObject.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;

        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            Vector3 movePos = transform.position;
            movePos.y = hit.point.y + groundDist;
            transform.position = movePos;
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(x, 0, y).normalized;

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? runningSpeed : walkingSpeed;

        rb.velocity = moveDir * currentSpeed;

        if (x != 0)
        {
            sr.flipX = x < 0;
        }

        bool isWalking = moveDir.magnitude > 0.1f;
        animator.SetBool("IsWalking", isWalking);
        animator.SetBool("IsRunning", currentSpeed == runningSpeed);
    }
}
