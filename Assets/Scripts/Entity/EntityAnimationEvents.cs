using UnityEngine;

public class EntityAnimationEvents : MonoBehaviour
{
    private Entity Entity;

    private void Start()
    {
        Entity = GetComponentInParent<Entity>();
    }

    public void OnAttackStarted()
    {
    }

    public void OnAttackEnded()
    {
    }

    public void OnDamageTarget()
    {
    }

    public void OnDeath()
    {
    }
}
