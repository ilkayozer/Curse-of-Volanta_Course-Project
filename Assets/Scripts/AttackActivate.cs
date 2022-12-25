using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackActivate : MonoBehaviour
{
    public Vector2 attackSize;


    public void AttackActive(string target)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, attackSize, 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == target)
            {
                //if (collider.GetComponent<PlayerMovement>().health == 0)
                //{
                //    StartCoroutine(collider.GetComponent<PlayerMovement>().Death());
                //}


                StartCoroutine(collider.GetComponent<Creature>().Hit());

            }
        }
    }
}
