using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBolt : MonoBehaviour
{
    public float energyBoltSpeed;
    public Rigidbody2D rb;

    private void FixedUpdate()
    {
        rb.AddForce(transform.right * energyBoltSpeed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(collision.GetComponent<Creature>().TakeDamage(20));
            Destroy(gameObject);
        }
    }

    
}
