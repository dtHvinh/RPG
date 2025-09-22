using UnityEngine;

public class Enemy : Entity
{
    private bool isTargetInSight;

    protected override void Update()
    {
        HandleMovement();
        HandleCollisionDetection();
    }

    protected override void HandleCollisionDetection()
    {
        base.HandleCollisionDetection();

        isTargetInSight = Physics2D.OverlapCircle(attackPoint.position, Stats.GetAttackRadius(), targetLayers);
    }

    protected void HandleMovement()
    {
        SetVelocity(
            xVelocity: FacingDirection * Stats.GetMoveSpeed(),
            yVelocity: Rb.linearVelocity.y);
    }
}
