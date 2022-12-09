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

    private bool canDash = true;
    private bool isDashing = false;
    public float dashForce = 10f;
    private float dashTime = 0.4f;
    private float dashCooldown = 0.1f;

    public float movementSpeed = 4f;
    public float jumpForce = 5f;

    private float offsetX = 0.47f;
    private float offsetY = -0.32f;
    private float dirX;

    private enum MovementState { idle, running, jumping, jumptofalling, dash}
    private MovementState state;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        boxcol = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        if (isDashing)
        {
            return;
        }

        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * movementSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && IsGrounded())
        {
            StartCoroutine(Dash());
        }

        UpdateAnimationState();

    }

    private void UpdateAnimationState()
    {
        

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

    private IEnumerator Dash()
    {
        canDash= false;
        isDashing= true;
        if (playerSprite.flipX)
        {
            rb.velocity = new Vector2(-dashForce, 0f);
        }
        else
        {
            rb.velocity = new Vector2(dashForce, 0f);
        }
        anim.Play("Dash");
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash= true;
    }

}
