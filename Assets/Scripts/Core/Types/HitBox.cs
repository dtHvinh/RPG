using UnityEngine;

public class HitBox : MonoBehaviour
{
    private IEntity entity;
    private IStats stats;
    private IHealth health;
    private ICounterable counterable;
    private EntityStatusHandler statusHandler;

    private void Awake()
    {
        entity = GetComponentInParent<IEntity>();
        stats = GetComponentInParent<IStats>();
        health = GetComponentInParent<IHealth>();
        counterable = GetComponentInParent<ICounterable>();
        statusHandler = GetComponentInParent<EntityStatusHandler>();
    }

    public IEntity GetEntity() => entity;
    public IStats GetStats() => stats;
    public IHealth GetHealth() => health;
    public ICounterable GetCounterable() => counterable;
    public EntityStatusHandler GetStatusHandler() => statusHandler;
}
