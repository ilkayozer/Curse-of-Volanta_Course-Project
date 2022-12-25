using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Player : Creature
{
    private Vector2 dir;

    private BoxCollider2D boxcol;

    public LayerMask jumpableGround;

    public float jumpForce = 1000f;

    void Start()
    {
        boxcol= GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        dir.y = 0;
    }

    
    void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        updateAnim.UpdateAnimation(dir);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + dir * movementSpeed * Time.fixedDeltaTime);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxcol.bounds.center, boxcol.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }



}
