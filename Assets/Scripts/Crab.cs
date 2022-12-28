using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : Creature
{
    public float crabMovementSpeed;

    public Transform playerTransform;
    private float dirEnemy;

    private HealthBar healthBar;
    private int maxHealth=100;
    private int hp;

    public GameObject ability;
    private bool canAttack = true;
    private bool isAttacking = false;
    private float attackTime = 1f;

    private enum MovementState { idle, running }
    private MovementState state;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hp = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

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

        if (dirEnemy < 1f && dirEnemy > -1f)
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
                rb.velocity = new Vector2(crabMovementSpeed, rb.velocity.y);
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                state = MovementState.running;
            }
            else if (dirEnemy < 0f)
            {
                rb.velocity = new Vector2(-crabMovementSpeed, rb.velocity.y);
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
            anim.Play("Crab_Attack_A");
            yield return new WaitForSeconds(0.4f);
            ability.GetComponent<EnemyAttack>().Attack();
            yield return new WaitForSeconds(attackTime - 0.4f);
            isAttacking = false;
            canAttack = true;
        }
    }

}
