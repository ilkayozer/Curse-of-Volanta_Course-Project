using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public int health = 100;

    public bool isHitting = false;
    public Animator anim;
    public Rigidbody2D rb;

    public bool isDead = false;

    public IEnumerator TakeDamage()
    {
        isHitting = true;
        anim.Play("TakeDamage");
        yield return new WaitForSeconds(0.5f);
        isHitting = false;
    }

    public IEnumerator Death()
    {
        rb.velocity = new Vector2(0f, 0f);
        isDead = true;
        anim.Play("Golem_Death_A");
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);

    }
}
