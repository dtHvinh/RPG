using UnityEngine;

public class HitBox : MonoBehaviour
{
    private IEntity entity;

    private void Awake()
    {
        entity = GetComponentInParent<IEntity>();
    }

    public IEntity GetEntity()
    {
        return entity;
    }

    public IHealth GetHealth()
    {
        return GetComponentInParent<IHealth>();
    }

    public ICounterable GetCounterable()
    {
        return GetComponentInParent<ICounterable>();
    }
}
