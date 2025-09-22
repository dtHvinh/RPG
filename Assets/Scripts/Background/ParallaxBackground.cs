using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Camera mainCamera;
    private float lastCameraPositionX;
    private float cameraHalfWidth;

    [SerializeField] private ParallaxLayer[] layers;

    private void Awake()
    {
        mainCamera = Camera.main;
        cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
    }

    private void LateUpdate()
    {
        float deltaX = GetMovementDeltaX();
        float cameraLeftX = mainCamera.transform.position.x - cameraHalfWidth;
        float cameraRightX = mainCamera.transform.position.x + cameraHalfWidth;

        foreach (ParallaxLayer layer in layers)
        {
            layer.MoveLayer(deltaX, 0);
        }
    }

    private float GetMovementDeltaX()
    {
        float deltaX = mainCamera.transform.position.x - lastCameraPositionX;
        lastCameraPositionX = mainCamera.transform.position.x;

        return deltaX;
    }
}
