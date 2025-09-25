using UnityEngine;

public class EntityAnimationTriggers : MonoBehaviour
{
    protected Entity entity;

    protected virtual void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    protected void CurrentStateTrigger()
    {
        entity.CurrentStateAnimationTrigger();
    }
}
