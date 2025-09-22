using UnityEngine;

public abstract class Enemy : Entity
{
    public Enemy_IdleState IdleState { get; protected set; }
    public Enemy_MoveState MoveState { get; protected set; }
}
