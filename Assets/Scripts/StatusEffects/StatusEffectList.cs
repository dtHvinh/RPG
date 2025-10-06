using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatusEffectList
{
    [SerializeReference] private List<StatusEffect> effects;
    private float _accumulator = 0;

    public event EventHandler<StatusEffectStartEventArgs> OnEffectStart;
    public event EventHandler<StatusEffectEndEventArgs> OnEffectEnd;


    public StatusEffectList()
    {
        effects = new List<StatusEffect>();
    }

    public void OnTick(float delta)
    {
        _accumulator += delta;

        for (int i = effects.Count - 1; i >= 0; i--)
        {
            var effect = effects[i];
            bool isSecond = false;

            if (_accumulator >= 1f)
            {
                isSecond = true;
                _accumulator -= 1;
            }

            effect.OnTick(delta, isSecond);

            if (effect.IsExpired)
            {
                effect.EffectEnd();
                OnEffectEnd?.Invoke(this, StatusEffectEndEventArgs.Create(effect));
                effects.RemoveAt(i);
            }
        }
    }

    public void AddEffect(StatusEffect newEffect)
    {
        if (newEffect.EffectType == EffectType.Unique)
        {
            if (SearchForEffect(newEffect, out StatusEffect exist)
                && newEffect.CompareTo(exist) >= 0)
            {
                newEffect.OverrideEffect(exist);
            }
            else
            {
                effects.Add(newEffect);
            }
        }
        else if (newEffect.EffectType == EffectType.Stackable)
        {
            effects.Add(newEffect);
        }

        newEffect.EffectStart();
        OnEffectStart?.Invoke(this, StatusEffectStartEventArgs.Create(newEffect));
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

    public override string ToString()
    {
        return $"Currently have {effects.Count} effect(s)";
    }
}

public class StatusEffectStartEventArgs
{
    public StatusEffect Effect;

    public static StatusEffectStartEventArgs Create(StatusEffect effect) => new() { Effect = effect };
}

public class StatusEffectEndEventArgs
{
    public StatusEffect Effect;

    public static StatusEffectEndEventArgs Create(StatusEffect effect) => new() { Effect = effect };
}