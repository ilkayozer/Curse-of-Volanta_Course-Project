using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public int health;

    public bool isHitting = false;
    public Animator anim;
    public Rigidbody2D rb;

    public bool isDead = false;

    public GameObject gem;

    public HealthBar healthBar;
    public int maxHealth;
    public int currentHealth;

    public IEnumerator TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            StartCoroutine(Death());
        }
        else
        {
            isHitting = true;
            anim.Play("TakeDamage");
            yield return new WaitForSeconds(0.8f);
            isHitting = false;
        }
        
    }

    public IEnumerator Death()
    {
        rb.velocity = new Vector2(0f, 0f);
        isDead = true;
        anim.Play("Death");
        Instantiate(gem, transform.position, transform.rotation);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);       
    }
}
