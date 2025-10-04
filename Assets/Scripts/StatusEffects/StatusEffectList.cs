using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatusEffectList
{
    [SerializeReference] private List<StatusEffect> effects;

    public StatusEffectList()
    {
        effects = new List<StatusEffect>();
    }

    public void Tick()
    {
        for (int i = effects.Count - 1; i >= 0; i--)
        {
            var effect = effects[i];
            effect.Tick();

            if (effect.IsExpired)
            {
                effect.EffectEnd();
                effects.RemoveAt(i);
            }
        }
    }

    public void AddEffect(StatusEffect newEffect)
    {
        if (newEffect.EffectType == EffectType.Unique)
        {
            if (SearchForEffect(newEffect, out StatusEffect exist))
            {
                newEffect.OverrideEffect(exist);
            }
            else
            {
                effects.Add(newEffect);
            }
        }

        newEffect.EffectStart();
    }

    private bool SearchForEffect(StatusEffect search, out StatusEffect effect)
    {
        foreach (var e in effects)
        {
            if (e.EffectTypeId == search.EffectTypeId)
            {
                effect = e;
                return true;
            }
        }

        effect = null;
        return false;
    }
}
