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
                collider.GetComponent<Player>().health -= 10;
                if (collider.GetComponent<Player>().health <= 0)
                {
                    StartCoroutine(collider.GetComponent<Player>().Death());
                }
                else
                {
                    StartCoroutine(collider.GetComponent<Player>().PlayerHit());
                }
            }
        }
    }
}
