using System.Collections;
using UnityEngine;

public class EntityVFX : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private EntityStatusHandler statusHandler;

    [Header("Colors")]
    [SerializeField] protected Color hitColor;
    [SerializeField] protected Color slowColor = Color.cyan;
    [SerializeField] protected Color burnColor = Color.red;

    [Header("Effects")]
    [SerializeField] private AfterimageTrail trailEffect;

    private Coroutine slowCo;
    private Coroutine burnCo;

    public virtual void Awake()
    {
    }

    public virtual void Update()
    {
        if (statusHandler != null && spriteRenderer != null)
        {
            statusHandler.Effects.OnEffectStart += Effects_OnEffectStart;
            statusHandler.Effects.OnEffectEnd += Effects_OnEffectEnd;
        }
    }

    private void Effects_OnEffectEnd(object sender, StatusEffectEndEventArgs e)
    {
        if (e.Effect is SlowEffect)
        {
            StopCoroutine(slowCo);
            spriteRenderer.color = Color.white;
        }
        else if (e.Effect is BurnEffect)
        {
            StopCoroutine(burnCo);
            spriteRenderer.color = Color.white;
        }
        else if (e.Effect is SpeedUpEffect)
        {
            if (trailEffect != null)
                trailEffect.Enable(false);
        }
    }

    private void Effects_OnEffectStart(object sender, StatusEffectStartEventArgs e)
    {
        if (e.Effect is SlowEffect slowEffect)
        {
            if (slowCo != null)
                StopCoroutine(slowCo);

            slowCo = StartCoroutine(StartSlowVFXCoroutine(slowEffect.Duration));
        }
        else if (e.Effect is BurnEffect burnEffect)
        {
            if (burnCo != null)
                StopCoroutine(burnCo);

            burnCo = StartCoroutine(StartBurnVFXCoroutine(burnEffect.Duration));
        }
        else if (e.Effect is SpeedUpEffect)
        {
            if (trailEffect != null)
                trailEffect.Enable(true);
        }
    }

    private IEnumerator StartSlowVFXCoroutine(float duration)
    {
        float tickInterval = .2f;
        float hasPassed = 0;

        Color light = slowColor * 1.2f;
        Color dark = slowColor * 0.8f;

        bool toggle = false;

        while (hasPassed < duration)
        {
            spriteRenderer.color = toggle ? light : dark;

            yield return new WaitForSeconds(tickInterval);

            toggle = !toggle;
            hasPassed += tickInterval;
        }

        spriteRenderer.color = Color.white;
    }

    private IEnumerator StartBurnVFXCoroutine(float duration)
    {
        float tickInterval = .2f;
        float hasPassed = 0;

        Color light = burnColor * 1.2f;
        Color dark = burnColor * 0.8f;

        bool toggle = false;

        while (hasPassed < duration)
        {
            spriteRenderer.color = toggle ? light : dark;

            yield return new WaitForSeconds(tickInterval);

            toggle = !toggle;
            hasPassed += tickInterval;
        }

        spriteRenderer.color = Color.white;
    }
}