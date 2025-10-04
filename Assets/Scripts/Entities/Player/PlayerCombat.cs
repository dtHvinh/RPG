using UnityEngine;

public class PlayerCombat : EntityCombat
{
    [Header("Counter attack details")]
    [SerializeField] private float counterAttackWindow;

    protected override void Awake()
    {
        base.Awake();

    }

    public float GetCounterAttackWindow() => counterAttackWindow;

    public bool CounterAttackPerformed()
    {
        bool hasCounterPerfomed = false;

        int count = GetHitBoxes(stats.AttackRadius);

        for (int i = 0; i < count; i++)
        {
            HitBox hitBox = GetHitBoxes()[i];

            ICounterable counterable = hitBox.GetCounterable();
            if (counterable != null
                && counterable.CanBeCountered)
            {
                hasCounterPerfomed = true;
                counterable.HandleCounter();
            }
        }

        return hasCounterPerfomed;
    }
}
