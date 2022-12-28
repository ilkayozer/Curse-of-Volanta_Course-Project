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
                StartCoroutine(collider.GetComponent<Player>().PlayerHit());
            }
        }
    }
}
