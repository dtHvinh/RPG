using UnityEngine;

public interface IEntity
{
    Rigidbody2D Rb { get; }
    Animator Animator { get; }
}

public interface IEntity<TCombat, TMovement, TCollision, THealth> : IEntity
    where TCombat : EntityCombat
    where TMovement : EntityMovement
    where TCollision : EntityCollision
    where THealth : EntityHealth
{
    TCombat Combat { get; }
    TMovement Movement { get; }
    TCollision Collision { get; }
    THealth Health { get; }
}