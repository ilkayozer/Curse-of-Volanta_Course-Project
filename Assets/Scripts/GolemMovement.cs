using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GolemMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer enemySprite;
    public float enemyMovementSpeed;

    public Transform playerTransform;
    private float dirEnemy;

    private enum MovementState { idle, running}
    private MovementState state;

    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
        enemySprite= GetComponent<SpriteRenderer>();
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
                rb.velocity = new Vector2(enemyMovementSpeed, rb.velocity.y);
                enemySprite.flipX = false;
                state = MovementState.running;
            }
            else if (dirEnemy < 0f)
            {
                rb.velocity = new Vector2(-enemyMovementSpeed, rb.velocity.y);
                enemySprite.flipX = true;
                state = MovementState.running;
            }
        }
        else
        {
            state = MovementState.idle;
        }

        anim.SetInteger("state", (int)state);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sword")
        {
            rb.velocity = new Vector2(0f, 0f);
            StartCoroutine(Death());
        }
    }

    private IEnumerator Death()
    {
        anim.Play("Golem_Death_A");
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);

    }
}
