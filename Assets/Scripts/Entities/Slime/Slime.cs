using System;
using UnityEngine;

public class Slime : Enemy, ICounterable
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

    [ContextMenu("Counter")]
    public void HandleCounter()
    {
        StateMachine.ChangeState(StunnedState);

        OnCounter?.Invoke(this, EventArgs.Empty);
    }
}
