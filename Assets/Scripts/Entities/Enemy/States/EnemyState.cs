using UnityEngine;

public class EnemyState : EntityState
{
    protected EnemyBase enemy;

    public EnemyState(EntityStateMachine stateMachine, EnemyBase entity, string animationBoolName)
        : base(stateMachine, entity, animationBoolName)
    {
        enemy = entity;
    }

    public override void UpdateAnimationParameters()
    {
        Animator.SetFloat(AnimatorConstants.X_VELOCITY, Mathf.Abs(enemy.Rb.linearVelocityX));
        Animator.SetFloat(AnimatorConstants.BATTLE_ANIM_SPEED_MULTI, enemy.BattleAnimSpeedMulti);
        Animator.SetFloat(AnimatorConstants.MOVE_ANIM_SPEED_MULTI, enemy.MoveAnimSpeedMulti);
    }
}
