using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EntityVFX : MonoBehaviour
{
    [SerializeField] private EntityStats entityStats;
    private Material originalMaterial;
    private SpriteRenderer sr;

    [Header("On Damage VFX")]
    [SerializeField] private Material onDamageVFXMaterial;
    [SerializeField] private float onDamageVFXDuration;

    private Coroutine flashRoutine;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalMaterial = sr.material;
    }

    public void PlayOnDamageVFX()
    {
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);
        flashRoutine = StartCoroutine(OnDamageVFXRoutine());
    }

    private System.Collections.IEnumerator OnDamageVFXRoutine()
    {
        sr.material = onDamageVFXMaterial;
        yield return new WaitForSeconds(onDamageVFXDuration);
        sr.material = originalMaterial;
    }
}
