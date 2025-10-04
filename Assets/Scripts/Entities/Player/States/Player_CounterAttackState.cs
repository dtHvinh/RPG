public class Player_CounterAttackState : PlayerState
{
    public const string STATE_NAME = "counterAttack";
    private bool counterAny = false;

    public Player_CounterAttackState(EntityStateMachine stateMachine, Player player, string animationBoolName)
        : base(stateMachine, player, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        SetTimer(player.Combat.GetCounterAttackWindow());

        counterAny = player.Combat.CounterAttackPerformed();
        player.Animator.SetBool("counterAttackPerformed", counterAny);
    }

    public override void Update()
    {
        base.Update();

        player.Movement.Stop();

        if (triggerCalled)
            stateMachine.ChangeState(player.IdleState);

        if (IsTimerFinished() && !counterAny)
            stateMachine.ChangeState(player.IdleState);
    }

    public override void Exit()
    {
        base.Exit();

        player.Animator.SetBool("counterAttackPerformed", false);
    }
}
