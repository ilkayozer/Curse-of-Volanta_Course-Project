using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAnimations : MonoBehaviour
{
    public enum MovementState { idle, running, jumping, jumptofalling }
    public Rigidbody2D rb;
    public Animator anim;

    public Transform playerTransform;
    private float dirEnemy;

    private bool canAttack = true;
    private bool isAttacking = false;
    private bool isDead = false;

    private int movementSpeed;

    private bool isHitting = false;

    private MovementState state;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }

        dirEnemy = playerTransform.position.x - transform.position.x;

        if (isAttacking)
        {
            return;
        }

        if (dirEnemy<1f && dirEnemy > 1f)
        {
            state = MovementState.idle;
            rb.velocity = new Vector2(0f, 0f);
            if (canAttack)
            {
                StartCoroutine(Attack());
            }
        }
        else if (dirEnemy < 10f && dirEnemy > -10f)
        {
            if (dirEnemy > 0f)
            {
                rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                state = MovementState.running;
            }
            else if (dirEnemy < 0f)
            {
                rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                state = MovementState.running;
            }
        }
        else
        {
            state = MovementState.idle;
        }

        anim.SetInteger("state", (int)state);
    }

    
}
