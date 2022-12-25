using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Creature : MonoBehaviour
{
    public bool canAttack;
    public bool isAttacking;
    public float attackTime;

    public bool isHitting;

    public bool isDead;

    public Animator anim;
    public Rigidbody2D rb;

    public AttackActivate ability;

    public float movementSpeed;

    public UpdateAnimationState updateAnim;

    public IEnumerator AttackAnim(string target)
    {
        if (!isHitting)
        {
            canAttack = false;
            isAttacking = true;

            anim.Play("Attack");
            yield return new WaitForSeconds(0.3f);
            ability.AttackActive(target);
            yield return new WaitForSeconds(attackTime - 0.3f);
            isAttacking = false;
        }
        canAttack = true;
    }

    public IEnumerator Hit()
    {
        //if (!isDashing)
        //{
        //    isHitting = true;
        //    anim.Play("Hurt");
        yield return new WaitForSeconds(0.4f);
        //}
        //isHitting = false;
    }

    public IEnumerator Death()
    {
        rb.velocity = new Vector2(0f, 0f);
        isDead = true;
        anim.Play("Death");
        yield return new WaitForSeconds(1.1f);
        Destroy(gameObject);
    }


}
