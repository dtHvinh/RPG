using UnityEngine;

public class Enemy : Entity
{
    private bool isTargetInSight;

    protected override void Update()
    {
        HandleMovement();
        HandleCollisionDetection();
        HandleAttack();
    }

    protected void HandleAttack()
    {
        if (isTargetInSight)
        {
            Animator.SetTrigger(AnimatorConstants.ATTACK);
        }
    }

    protected override void HandleCollisionDetection()
    {
        base.HandleCollisionDetection();

        isTargetInSight = Physics2D.OverlapCircle(attackPoint.position, Stats.GetAttackRadius(), targetLayers);
    }

    protected void HandleMovement()
    {
        if (CanMove)
            SetVelocity(
                xVelocity: FacingDirection * Stats.GetMoveSpeed(),
                yVelocity: Rb.linearVelocity.y);
        else
            SetVelocity(
                xVelocity: 0,
                yVelocity: Rb.linearVelocity.y);
    }
}
