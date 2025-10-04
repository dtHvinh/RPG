using System;

public class Skeleton : Enemy, ICounterable
{
    public bool CanBeCountered { get => Combat.CanBeCounter(); }

    public event EventHandler OnCounter;

    public override void InitializeStates()
    {
        IdleState = new Enemy_IdleState(StateMachine, this, "idle");
        MoveState = new Enemy_MoveState(StateMachine, this, "move");
        AttackState = new Enemy_AttackState(StateMachine, this, "attack");
        BattleState = new Enemy_BattleState(StateMachine, this, "battle");
        HurtState = new Enemy_HurtState(StateMachine, this, "hurt");
        DeathState = new Enemy_DeathState(StateMachine, this, "death");
        StunnedState = new Enemy_StunnedState(StateMachine, this, "stunned");
    }

    public void HandleCounter()
    {
        if (CanBeCountered)
        {
            OnCounter?.Invoke(this, EventArgs.Empty);
            Combat.EnableCounterWindow(false);
            StateMachine.ChangeState(StunnedState);
        }
    }
}
