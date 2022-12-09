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
    private Vector2 dirVec;
    private Vector2 playerPos;
    private Vector2 enemyPos;

    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
        enemySprite= GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        playerPos = playerTransform.position;
        enemyPos = transform.position;   
        dirVec = enemyPos - playerPos;

        rb.velocity = new Vector2(-dirVec.x * enemyMovementSpeed, rb.velocity.y);
    }

}
