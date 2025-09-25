using UnityEngine;

public class EnemyState : EntityState
{
    protected Enemy enemy;

    public EnemyState(EntityStateMachine stateMachine, Enemy enemy, string animationBoolName)
        : base(stateMachine, enemy, animationBoolName)
    {
        this.enemy = enemy;
    }

    public override void UpdateAnimationParameters()
    {
        Animator.SetFloat(AnimatorConstants.X_VELOCITY, Mathf.Abs(enemy.Rb.linearVelocityX));
        Animator.SetFloat(AnimatorConstants.BATTLE_ANIM_SPEED_MULTI, enemy.BattleAnimSpeedMulti);
        Animator.SetFloat(AnimatorConstants.MOVE_ANIM_SPEED_MULTI, enemy.MoveAnimSpeedMulti);
    }
}
