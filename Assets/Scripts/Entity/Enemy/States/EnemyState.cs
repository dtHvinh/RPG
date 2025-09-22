using UnityEngine;

public class EnemyState : EntityState
{
    protected Enemy enemy;

    public EnemyState(EntityStateMachine stateMachine, Enemy entity, string animationBoolName) 
        : base(stateMachine, entity, animationBoolName)
    {
        enemy = entity;
    }
}
