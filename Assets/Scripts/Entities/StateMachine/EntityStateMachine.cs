using Unity.VisualScripting;
using UnityEngine;

public class EntityStateMachine
{
    public EntityState CurrentState { get; private set; } 

    public void Initialize(EntityState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(EntityState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    public void ActiveStateUpdate()
    {
        CurrentState.Update();
    }
}
