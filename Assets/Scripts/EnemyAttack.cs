using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Vector2 attackSize;


    public void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, attackSize, 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Player")
            {
                if (collider.GetComponent<PlayerMovement>().health == 0)
                {
                    StartCoroutine(collider.GetComponent<PlayerMovement>().Death());
                }
                else
                {
                    StartCoroutine(collider.GetComponent<PlayerMovement>().PlayerHit());
                }
            }
        }
    }
}
