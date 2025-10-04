using UnityEngine;

public abstract class EntityState
{
    protected IEntity entity;
    protected EntityStateMachine stateMachine;

    protected Rigidbody2D Rb;
    protected Animator Animator;

    public string AnimationBoolName { get; protected set; }

    private float stateTimer;
    protected bool triggerCalled;

    protected EntityState(EntityStateMachine stateMachine, IEntity entity, string animationBoolName)
    {
        this.entity = entity;

        this.stateMachine = stateMachine;
        AnimationBoolName = animationBoolName;

        Rb = entity.Rb;
        Animator = entity.Animator;
    }

    public virtual void Enter()
    {
        Animator.SetBool(AnimationBoolName, true);
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer = Mathf.Max(stateTimer - Time.deltaTime, 0f);

        UpdateAnimationParameters();
    }

    public void SetTimer(float time) => stateTimer = time;

    public bool IsTimerFinished() => stateTimer <= 0;

    public virtual void Exit()
    {
        Animator.SetBool(AnimationBoolName, false);
    }

    public void CurrentStateAnimationTrigger()
    {
        triggerCalled = true;
    }

    public virtual void UpdateAnimationParameters()
    {
    }
}
