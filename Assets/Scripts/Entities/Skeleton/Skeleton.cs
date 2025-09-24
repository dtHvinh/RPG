public class Skeleton : EnemyBase
{
    public override void Awake()
    {
        base.Awake();

        AI = new EnemyAI(this);
    }

    public override void InitializeStates()
    {
        IdleState = new Enemy_IdleState(StateMachine, this, "idle");
        MoveState = new Enemy_MoveState(StateMachine, this, "move");
        AttackState = new Enemy_AttackState(StateMachine, this, "attack");
        BattleState = new Enemy_BattleState(StateMachine, this, "battle");
    }

    public override void SetInitialState()
    {
        StateMachine.Initialize(MoveState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
