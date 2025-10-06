using System;

public interface IHealth : IDamable
{
    event EventHandler<EntityHurtEventArgs> OnHurt;
    event EventHandler<DeathEventArgrs> OnDeath;
    event EventHandler<HealthChangeEventArgs> OnHealthChange;

    void Regenerate(float amount);
}
