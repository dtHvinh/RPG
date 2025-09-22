using UnityEngine;

public class EnemyState : EntityState
{
    protected Enemy enemy;

    public EnemyState(EntityStateMachine stateMachine, Enemy entity, string animationBoolName) 
        : base(stateMachine, entity, animationBoolName)
    {
        enemy = entity;
    }

    public override void Update()
    {
        base.Update();

        Animator.SetFloat(AnimatorConstants.MOVE_ANIM_SPEED_MULTI, enemy.moveAnimSpeedMulti);
    }
}
