using UnityEngine;

public class Enemy : Entity
{
    private bool isTargetInSight;

    protected override void Update()
    {
        HandleMovement();
        HandleAnimations();
        HandleCollision();
        HandleFlip();
        HandleAttack();
    }

    protected override void HandleAttack()
    {
        if (isTargetInSight)
        {
            animator.SetTrigger(AnimatorConstants.ATTACK);
        }
    }

    protected override void HandleAnimations()
    {
        animator.SetFloat(AnimatorConstants.X_VELOCITY, rb.linearVelocity.x);
    }

    protected override void HandleCollision()
    {
        base.HandleCollision();

        isTargetInSight = Physics2D.OverlapCircle(attackPoint.position, stats.GetAttackRadius(), targetLayers);
    }

    protected override void HandleMovement()
    {
        if (canMove)
            rb.linearVelocity = new Vector2(facingDirection * moveSpeed, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }
}
