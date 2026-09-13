using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // check if Rigidbody2D and Animator components are attached to the player
        if (rb == null)
        Debug.LogError("Missing Rigidbody2D!");

    if (animator == null)
        Debug.LogError("Missing Animator!");
    }

    void Update()
    {
        // check player input from keyboard
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // update player animation based on movement direction
        if (movement.sqrMagnitude > 0.01f)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }

        // update player animation based on movement speed
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        // move the player based on input and speed
        if (movement.sqrMagnitude > 0.01f)
        {
            rb.linearVelocity = movement.normalized * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero; 
        } 
    }
}