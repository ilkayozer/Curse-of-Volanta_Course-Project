using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxcol;
    public LayerMask jumpableGround;

    private HealthBar healthBar;
    public int health = 100;
    private int maxHealth = 100;

    private bool canDash = true;
    private bool isDashing = false;
    public float dashForce = 10f;
    private float dashTime = 0.3f;
    private float dashCooldown = 0.4f;

    private bool canAttack = true;
    private bool isAttacking = false;
    private float attackTime = 0.6f;
    private bool isHitting = false;
    private bool isDead = false;

    public float playerMovementSpeed = 4f;
    public float jumpForce = 5f;

    public GameObject sword;
    private float dirX;

    private enum MovementState { idle, running, jumping, jumptofalling}
    private MovementState state;

    void Start()
    {
        boxcol= GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //healthBar.SetMaxHealth(maxHealth);
    }


    void Update()
    {
        if (transform.position.y < -20)
        {
            Death();
        }

        if (isDashing || isAttacking)
        {
            return;
        }

        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * playerMovementSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && IsGrounded())
        {
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack && IsGrounded())
        {
            rb.velocity = new Vector2(0f, 0f);
            StartCoroutine(Attack());
        }

        UpdateAnimationState();

    }

    private void UpdateAnimationState()
    {
        if (dirX > 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);           
            state = MovementState.running;
        }
        else if (dirX < 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            state = MovementState.running;
        }
        else
        {
            state= MovementState.idle;
        }

        if (rb.velocity.y > 0.001f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.001f)
        {
            state = MovementState.jumptofalling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxcol.bounds.center, boxcol.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    private IEnumerator Dash()
    {
        canDash= false;
        isDashing= true;
        if (transform.rotation.eulerAngles.y == 180f)
        {
            rb.velocity = new Vector2(-dashForce, 0f);
        }
        else
        {
            rb.velocity = new Vector2(dashForce, 0f);
        }
        anim.Play("Dash");
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash= true;
    }

    private IEnumerator Attack()
    {
        if (!isHitting)
        {
            canAttack = false;
            isAttacking = true;

            anim.Play("Attack");
            yield return new WaitForSeconds(0.3f);
            sword.GetComponent<SwordSwing>().Attack();
            yield return new WaitForSeconds(attackTime - 0.3f);
            isAttacking = false;         
        }
        canAttack = true;
    }

    public IEnumerator PlayerHit()
    {
        if (!isDashing)
        {
            isHitting = true;
            anim.Play("Hurt");
            yield return new WaitForSeconds(0.4f);
        }
        isHitting = false;
    }

    public IEnumerator Death()
    {
        rb.velocity = new Vector2(0f, 0f);
        isDead = true;
        anim.Play("Death");
        yield return new WaitForSeconds(1.1f);
        Invoke(nameof(ReloadLevel), 0.5f);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}