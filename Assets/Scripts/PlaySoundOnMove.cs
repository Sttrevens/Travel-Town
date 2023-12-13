using UnityEngine;

public class PlaySoundOnMove : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component
    public Animator animator;       // Reference to the Animator component
    public float movementThreshold = 0.01f; // Minimum movement to trigger sound and animation

    private Vector3 lastPosition; // Last frame's position
    private Vector2 moveInput;    // Player's movement input

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        lastPosition = transform.position;
    }

    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        bool isMoving = moveInput.magnitude > 0;

        // Check if the GameObject has moved
        if (Vector3.Distance(transform.position, lastPosition) > movementThreshold)
        {
            if (isMoving && !audioSource.isPlaying)
            {
                audioSource.Play(); // Play sound
            }

            animator.SetBool("IsWalking", isMoving); // Play "PlayerWalk" animation when moving

            // Flip the player sprite based on the movement direction
            if (moveInput.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1); // Facing right
            }
            else if (moveInput.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // Facing left
            }
        }

        lastPosition = transform.position; // Update last position for the next frame
    }
}