using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GolemMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer golemSprite;
    public float golemMovementSpeed;

    public Transform playerTransform;
    private float dirEnemy;

    private enum MovementState { idle, running }
    private MovementState state;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        golemSprite = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        dirEnemy = playerTransform.position.x - transform.position.x;


        if (dirEnemy < 1f && dirEnemy > -1f)
        {
            rb.velocity = new Vector2(0f, 0f);
            state = MovementState.idle;
        }

        else if (dirEnemy < 20f && dirEnemy > -20f)
        {
            if (dirEnemy > 0f)
            {
                rb.velocity = new Vector2(golemMovementSpeed, rb.velocity.y);
                golemSprite.flipX = false;
                state = MovementState.running;
            }
            else if (dirEnemy < 0f)
            {
                rb.velocity = new Vector2(-golemMovementSpeed, rb.velocity.y);
                golemSprite.flipX = true;
                state = MovementState.running;
            }
        }
        else
        {
            state = MovementState.idle;
        }

        
        anim.SetInteger("state", (int)state);

    }

    public IEnumerator Death()
    {
        anim.Play("Golem_Death_A");
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);

    }

}
