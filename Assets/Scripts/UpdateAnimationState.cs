using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAnimationState : CreatureAnimations
{

    public void UpdateAnimation(Vector2 direction)
    {
        MovementState state;

        if (direction.x > 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            state = MovementState.running;
        }
        else if (direction.x < 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
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
}
