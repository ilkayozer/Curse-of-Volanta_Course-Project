using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer playerSprite;
    private BoxCollider2D boxcol;
    public LayerMask jumpableGround;

    public float movementSpeed = 4f;
    public float jumpForce = 5f;
    public float dashForce = 10f;

    private bool dashActive;
    private float offsetX = 0.47f;
    private float offsetY = -0.32f;
    private float dirX;

    private enum MovementState { idle, running, jumping, jumptofalling, dash}

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        boxcol = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * movementSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        /*if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashActive= true;
            if (playerSprite.flipX == false)
            {
                rb.velocity = new Vector2(dashForce, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-dashForce, rb.velocity.y);
            }
        }*/

        UpdateAnimationState();
        dashActive= false;
    }

    private void UpdateAnimationState()
    {
        MovementState state;


        if (dirX > 0f)
        {
            boxcol.offset = new Vector2(-offsetX, offsetY);
            playerSprite.flipX = false;
            state = MovementState.running;
        }
        else if (dirX < 0f)
        {
            boxcol.offset = new Vector2(offsetX, offsetY);
            playerSprite.flipX = true;
            state = MovementState.running;
        }
        else
        {
            state= MovementState.idle;
        }

        if (rb.velocity.y > 0.001f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.001f)
        {
            state = MovementState.jumptofalling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxcol.bounds.center, boxcol.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }
}
