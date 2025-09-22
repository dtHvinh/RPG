using UnityEngine;

public abstract class Enemy : Entity
{
    [Header("Enemy Movement Settings")]
    [Range(0, 2)] public float moveAnimSpeedMulti = 1f;
    public float IdleTime = 2f;

    public Enemy_IdleState IdleState { get; protected set; }
    public Enemy_MoveState MoveState { get; protected set; }
}
