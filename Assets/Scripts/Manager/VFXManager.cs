using System;
using UnityEngine;
using static EntityHurtEventArgs;

public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void ShowDamagePopup(Vector3 position, HurtInstance hurtInstance, SpawnParams spawnParams = default)
    {
        Vector3 spawnPos = position.ApplyRandomOffSet(spawnParams.xRandOffset, spawnParams.yRandOffset);
        DamagePopupPool.Instance.Spawn(spawnPos, (int)hurtInstance.FinalDamage, hurtInstance.DamageType, hurtInstance.IsCritical);
    }

    public void ShowHit(Vector3 position, Color color, SpawnParams spawnParams = default)
    {
        Vector3 spawnPos = position.ApplyRandomOffSet(spawnParams.xRandOffset, spawnParams.yRandOffset);
        HitVFXPool.Instance.Spawn(spawnPos, color);
    }

    public GameObject SpawnVFX(GameObject prefab, Vector3 position, Quaternion rotation = default)
    {
        if (prefab == null) return null;
        return Instantiate(prefab, position, rotation);
    }

    public void ShowGhost(SpriteRenderer mainRenderer, float lifetime)
    {
        GameObject ghost = AfterimageTrailPool.Instance.GetFromPool();
        ghost.GetComponent<TrailEffect>().Setup(mainRenderer, lifetime);
    }
}

[Serializable]
public struct SpawnParams
{
    public float xRandOffset;
    public float yRandOffset;

    public static SpawnParams Create(float xOffset = 0f, float yOffset = 0f)
    {
        return new SpawnParams { xRandOffset = xOffset, yRandOffset = yOffset };
    }
}
