using UnityEngine;

public class AreaBuffingObjectFloating : AreaBuffingObject
{
    [SerializeField] private FloatingObjectConfig floatingConfig;

    private void Update()
    {
        Vector3 tempPos = startPos;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * floatingConfig.FloatFrequency) * floatingConfig.FloatAmplitude;
        transform.position = tempPos;
    }
}
