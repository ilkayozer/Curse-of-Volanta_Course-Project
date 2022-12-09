using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer enemySprite;
    public float enemyMovementSpeed;

    public Transform playerTransform;
    private float dirEnemy;

    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
        enemySprite= GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        dirEnemy = playerTransform.position.x - transform.position.x;

        if (dirEnemy > 0f)
        {
            rb.velocity = new Vector2(enemyMovementSpeed, rb.velocity.y);
        }
        else if (dirEnemy < 0f)
        {
            rb.velocity = new Vector2(-enemyMovementSpeed, rb.velocity.y);
        }
        
    }

}
