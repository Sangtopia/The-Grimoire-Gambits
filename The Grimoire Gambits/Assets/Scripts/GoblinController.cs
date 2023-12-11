using System.Collections;
using UnityEngine;

public class GoblinController : MonoBehaviour
{
    public float speed;
    public float chaseRange;
    public float groundDist;
    public float wanderRange;
    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;
    public Animator animator;
    public Transform player;

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        initialPosition = transform.position;

        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            float moveDuration = Random.Range(1f, 3f);
            yield return MoveForDuration(moveDuration);

            float waitDuration = Random.Range(5f, 10f);
            yield return new WaitForSeconds(waitDuration);
        }
    }

    IEnumerator MoveForDuration(float duration)
    {
        float endTime = Time.time + duration;

        while (Time.time < endTime)
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

            // Check if player is within chase range
            if (player != null && Vector3.Distance(transform.position, player.position) <= chaseRange)
            {
                // Optionally, you can add behavior here if you want something specific to happen
                // when the player is within chase range but not being chased.
            }
            else
            {
                MoveToTarget();
                CheckFlip();
            }

            yield return null;
        }

        rb.velocity = Vector3.zero;
        isMoving = false;
        animator.SetBool("IsWalking", false);
    }

    void MoveToTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        rb.velocity = direction * speed;

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            PickNewTargetPosition();
        }
        else
        {
            isMoving = true;
        }

        animator.SetBool("IsWalking", isMoving);
    }

    void PickNewTargetPosition()
    {
        float randomX = Random.Range(-wanderRange, wanderRange);
        float randomZ = Random.Range(-wanderRange, wanderRange);

        targetPosition = initialPosition + new Vector3(randomX, 0, randomZ);

        sr.flipX = randomX < 0;
    }

    void CheckFlip()
    {
        if (rb.velocity.x != 0)
        {
            sr.flipX = rb.velocity.x < 0;
        }
    }
}
