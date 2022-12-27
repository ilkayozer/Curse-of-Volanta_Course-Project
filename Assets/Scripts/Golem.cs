using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Golem : Creature
{
    public float golemMovementSpeed;

    public Transform playerTransform;
    private float dirEnemy;

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
                rb.velocity = new Vector2(golemMovementSpeed, rb.velocity.y);
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                state = MovementState.running;
            }
            else if (dirEnemy < 0f)
            {
                rb.velocity = new Vector2(-golemMovementSpeed, rb.velocity.y);
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
            anim.Play("Golem_Attack_3");
            yield return new WaitForSeconds(0.4f);
            ability.GetComponent<EnemyAttack>().Attack();
            yield return new WaitForSeconds(attackTime - 0.4f);
            isAttacking = false;
            canAttack = true;
        }
    }

    

}
