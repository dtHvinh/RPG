using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected EntityStateMachine stateMachine;

    public Rigidbody2D Rb => player.Rb;
    public EntityStats Stats => player.Stats;
    public Vector2 MoveInput => player.MoveInput;
    public PlayerInputSet Inputs => player.Inputs;
    public Animator Animator => player.Animator;

    public string AnimationBoolName { get; private set; }

    protected float stateTimer;
    protected bool triggerCalled;

    public PlayerState(EntityStateMachine stateMachine, Player player, string animationBoolName)
    {
        this.stateMachine = stateMachine;
        AnimationBoolName = animationBoolName;
        this.player = player;
    }

    public virtual void Enter()
    {
        player.Animator.SetBool(AnimationBoolName, true);
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        player.Animator.SetFloat(AnimatorConstants.Y_VELOCITY, Rb.linearVelocityY);

        if (player.Inputs.Player.Dash.WasPressedThisFrame() && CanDash())
        {
            stateMachine.ChangeState(player.DashState);
        }
    }

    public virtual void Exit()
    {
        player.Animator.SetBool(AnimationBoolName, false);
    }

    public void CallAnimationTrigger()
    {
        triggerCalled = true;
    }

    private bool CanDash()
    {
        if(player.WallDetected)
            return false;

        if(stateMachine.CurrentState == player.DashState)
            return false;

        return true;
    }
}
