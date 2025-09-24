using System;
using UnityEngine;

[Serializable]
public class ParallaxLayer
{
    [SerializeField] private Transform layerTransform;
    [SerializeField] private float parallaxXFactor;
    [SerializeField] private float parallaxYFactor;

    public Transform LayerTransform => layerTransform;
    public float ParallaxXFactor => parallaxXFactor;
    public float ParallaxYFactor => parallaxYFactor;

    public void MoveLayer(float deltaX, float deltaY)
    {
        layerTransform.position += new Vector3(deltaX * parallaxXFactor, 0);
    }
}
