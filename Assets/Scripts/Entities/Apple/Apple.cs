using UnityEngine;

[RequireComponent(typeof(IHealth))]
public class Apple : Entity
{
    private IHealth health;
    public override float FacingDirection { get; }

    public override void Awake()
    {
        base.Awake();

        health = GetComponent<IHealth>();
    }

    public override void Death()
    {
        Destroy(gameObject);
    }

    public override void SetVelocity(float xVelocity, float yVelocity)
    {
        Debug.LogWarning("Apple cannot move.");
    }

    public override void SubscribeToEvents()
    {
        base.SubscribeToEvents();

        health.OnHurt += Health_OnHurt;
    }

    private void Health_OnHurt(object sender, EntityHurtEventArgs e)
    {
        VFXManager.Instance.ShowDamagePopup(transform.position, e.Hurt, SpawnParams.Create(.5f, .5f));
    }
}
