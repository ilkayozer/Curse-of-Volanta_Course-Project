using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Creature
{
    public float ratMovementSpeed;

    public Transform playerTransform;
    private float dirEnemy;

    public GameObject ability;
    private bool canAttack = true;
    private bool isAttacking = false;
    private float attackTime = 1f;

    public float attackForce;

    private bool yon;

    private enum MovementState { idle, running }
    private MovementState state;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


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

        if (dirEnemy < 1.5f && dirEnemy > -1.5f)
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
                yon = true;
                rb.velocity = new Vector2(ratMovementSpeed, rb.velocity.y);
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                state = MovementState.running;
            }
            else if (dirEnemy < 0f)
            {
                yon = false;
                rb.velocity = new Vector2(-ratMovementSpeed, rb.velocity.y);
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

    private IEnumerator Attack()
    {

        if (!isHitting)
        {
            canAttack = false;
            isAttacking = true;
            if (yon)
            {
                rb.velocity = new Vector2(attackForce, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-attackForce, rb.velocity.y);
            }
            anim.Play("Rat_Attack");
            yield return new WaitForSeconds(0.2f);
            ability.GetComponent<EnemyAttack>().Attack();
            yield return new WaitForSeconds(0.9f);
            isAttacking = false;
            canAttack = true;
        }
    }
}
