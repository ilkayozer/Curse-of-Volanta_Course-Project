using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    public Vector2 swordSize;

    public void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, swordSize, 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                collider.GetComponent<Creature>().health -= 10;
                if (collider.GetComponent<Creature>().health <= 0)
                {
                    StartCoroutine(collider.GetComponent<Creature>().Death());
                }
                else
                {
                    StartCoroutine(collider.GetComponent<Creature>().TakeDamage());                  
                }        
            }
        }
    }
}
