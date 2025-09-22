using UnityEngine;

public abstract class EntityState
{
    protected Entity entity;
    protected EntityStateMachine stateMachine;

    protected Rigidbody2D Rb;
    protected EntityStats Stats;
    protected Animator Animator;

    public string AnimationBoolName { get; protected set; }

    protected float stateTimer;
    protected bool triggerCalled;

    protected EntityState(EntityStateMachine stateMachine, Entity entity, string animationBoolName)
    {
        this.entity = entity;

        this.stateMachine = stateMachine;
        AnimationBoolName = animationBoolName;

        Rb = entity.Rb;
        Stats = entity.Stats;
        Animator = entity.Animator;
    }

    public virtual void Enter()
    {
        Animator.SetBool(AnimationBoolName, true);
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void Exit()
    {
        Animator.SetBool(AnimationBoolName, false);
    }

    public void CallAnimationTrigger()
    {
        triggerCalled = true;
    }
}
