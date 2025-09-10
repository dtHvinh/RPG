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
        Entity.SetCanMove(false);
        Entity.SetCanJump(false);
    }

    public void OnAttackEnded()
    {
        Entity.SetCanMove(true);
        Entity.SetCanJump(true);
    }

    public void OnDamageTarget()
    {
        Entity.DamageTargets();
    }

    public void OnDeath()
    {
        Entity.SetCanJump(false);
        Entity.SetCanMove(false);
    }
}
