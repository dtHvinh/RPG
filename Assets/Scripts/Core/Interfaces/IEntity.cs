using UnityEngine;

public interface IEntity
{
    Rigidbody2D Rb { get; }
    Animator Animator { get; }
}

public interface IEntity<TCombat, TMovement, TCollision, THealth, TStats>
    : IEntity, IHasCollision<TCollision>, IHasCombat<TCombat>, IHasMovement<TMovement>, IHasHealth<THealth>, IHasStats<TStats>
    where TCombat : EntityCombat
    where TMovement : EntityMovement
    where TCollision : EntityCollision
    where THealth : EntityHealth
    where TStats : EntityStats
{
}

public interface IEntity<TCombat, TMovement, TCollision, THealth>
    : IEntity, IHasCollision<TCollision>, IHasCombat<TCombat>, IHasMovement<TMovement>, IHasHealth<THealth>, IHasStats<EntityStats>
    where TCombat : EntityCombat
    where TMovement : EntityMovement
    where TCollision : EntityCollision
    where THealth : EntityHealth
{
    EntityStats Stats { get; }
}