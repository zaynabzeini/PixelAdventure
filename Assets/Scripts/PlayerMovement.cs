using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    
    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f; // [SerializeField] exposes it to the editor, like using public
    [SerializeField] private float jumpForce = 14f; // unlike public, other scripts still cant access these variables

    private enum MovementState { idle, running, jumping, falling } // we can now create variables with datastype MovementState, have the values 0, 1, 2, 3 respectively 

    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal"); // dirX now holds either + or - for right or left
        rb.velocity = new Vector2((dirX * moveSpeed), rb.velocity.y); // we use .y so it keeps its y position from one frame to the next

        if (Input.GetButtonDown("Jump") && IsGrounded()) // uses Input Manager 
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f) // moving left
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else 
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f) // if upwards force is applied
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y  < -.1f) // if downwards force is applied
        {
            state = MovementState.falling;
        }
        
        anim.SetInteger("state", (int)state); 
    }

    private bool IsGrounded() 
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        // creates a box on top of collider (using center and size), doesnt angle it (0f), offsets it down by .1f, and sees if it touches jumpableGround layer 
    }
}
