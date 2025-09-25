using UnityEngine;

public class EntityAnimationTriggers : MonoBehaviour
{
    private Entity entity;

    private void Start()
    {
        entity = GetComponentInParent<Entity>();
    }

    private void CurrentStateTrigger()
    {
        entity.CurrentStateAnimationTrigger();
    }

    private void AttackTrigger()
    {
        entity.Combat.PerformAttack();
    }
}
