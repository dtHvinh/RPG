using UnityEngine;

public class EnemyMovement : EntityMovement
{
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        entity = enemy;
    }

    public void MoveWithBaseSpeed(float direction)
    {
        enemy.SetVelocity(enemy.Stats.MoveSpeed * Mathf.Sign(direction), enemy.Rb.linearVelocityY);
    }

    public void MoveWithBaseSpeed()
    {
        enemy.SetVelocity(enemy.Stats.MoveSpeed * enemy.FacingDirection, enemy.Rb.linearVelocityY);
    }
}
