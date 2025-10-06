using UnityEngine;

public class AfterimageTrail : MonoBehaviour
{
    [SerializeField] private GameObject trailEffectPrefab;
    [SerializeField] private float spawnInterval = 0.05f;
    [SerializeField] private float ghostLifetime = 0.3f;

    [SerializeField] private SpriteRenderer mainRenderer;
    private IntervalTimer timer;
    private bool isActive = false;

    private void Awake()
    {
        timer = new();
    }

    private void Update()
    {
        if (!isActive || mainRenderer == null) return;

        timer.Update(Time.deltaTime);
        if (timer.IsElapsed(spawnInterval))
        {
            VFXManager.Instance.ShowGhost(mainRenderer, ghostLifetime);
        }
    }

    public void Enable(bool value)
    {
        isActive = value;
        timer.Reset();
    }
}
