using System.Collections;
using UnityEngine;

public class EntityAnimationTriggers : MonoBehaviour
{
    private EntityBase entity;

    private void Start()
    {
        entity = GetComponentInParent<EntityBase>();
    }

    private void CurrentStateTrigger()
    {
        entity.CurrentStateAnimationTrigger();
    }
}
