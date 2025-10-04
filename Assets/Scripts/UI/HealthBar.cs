using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private Image fillSlow;
    [SerializeField] private Transform border;

    [SerializeField] private ShakingInfo shakingInfo;
    [SerializeField] private SlowBarInfo slowBarInfo;

    private IHealth healthComponent;
    private Vector3 borderImageOriginal;
    private Coroutine slowBarCoroutine;

    private void Awake()
    {
        healthComponent = GetComponentInParent<IHealth>();

        borderImageOriginal = border.transform.localPosition;
    }

    private void Update()
    {
        if (shakingInfo.IsEnable
            && fillImage.fillAmount <= shakingInfo.Threshold)
        {
            float shakeX = UnityEngine.Random.Range(-shakingInfo.Magnitude, shakingInfo.Magnitude);
            float shakeY = UnityEngine.Random.Range(-shakingInfo.Magnitude, shakingInfo.Magnitude);
            border.transform.localPosition = new Vector3(shakeX, shakeY, 0);
        }
        else
        {
            border.transform.localPosition = borderImageOriginal;
        }
    }

    private void Start()
    {
        if (healthComponent == null)
        {
            Debug.LogError("HealthBar: No IHealth component found in parent.");
            return;
        }

        if (!slowBarInfo.IsEnable)
            fillSlow.gameObject.SetActive(false);

        fillImage.fillAmount = 1f;

        healthComponent.OnHealthChange += Health_OnHealthChange;
        healthComponent.OnDeath += HealthComponent_OnDeath;
    }

    private void HealthComponent_OnDeath(object sender, DeathEventArgrs e)
    {
        shakingInfo.IsEnable = false;
    }

    private void Health_OnHealthChange(object sender, HealthChangeEventArgs e)
    {
        fillImage.fillAmount = (float)Math.Round(e.CurrentHealth / e.MaxHealth, 2);
        SetSlowBar(fillImage.fillAmount);
    }

    private void SetSlowBar(float fill)
    {
        if (!slowBarInfo.IsEnable)
            return;

        if (slowBarCoroutine != null)
        {
            StopCoroutine(slowBarCoroutine);
        }
        slowBarCoroutine = StartCoroutine(LerpFill(fillSlow.fillAmount, fill, slowBarInfo.Duration));
    }

    private System.Collections.IEnumerator LerpFill(float from, float to, float duration)
    {
        yield return new WaitForSeconds(slowBarInfo.StartDelay);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            fillSlow.fillAmount = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }
        fillSlow.fillAmount = to;
    }
}

[Serializable]
public struct ShakingInfo
{
    public bool IsEnable;
    [Range(0, 1), Tooltip("The thresold that the shaking animation start")] public float Threshold;
    [Range(0, 2)] public float Magnitude;
}

[Serializable]
public struct SlowBarInfo
{
    public bool IsEnable;
    public float Duration;
    public float StartDelay;
}