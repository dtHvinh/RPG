using UnityEngine;

public class Skeleton : Enemy
{
    public override void InitializeStates()
    {
        IdleState = new Enemy_IdleState(stateMachine, this, "idle");
        MoveState = new Enemy_MoveState(stateMachine, this, "move");
    }

    public override void SetInitialState()
    {
        stateMachine.Initialize(MoveState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
