using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private ICollision collision;
    private const float CliffHeightThreshold = 4.0f;

    private void Awake()
    {
        collision = GetComponentInChildren<ICollision>();
    }

    public bool ShouldKeepChasingTarget()
    {
        return !IsCliffTooHigh();
    }

    public bool ShouldStartChasingTarget()
    {
        if (collision.CliffDetected)
        {
            return !IsCliffTooHigh();
        }

        return true;
    }

    private bool IsCliffTooHigh()
    {
        if (collision.CliffDetected)
        {
            RaycastHit2D hit = Physics2D.Raycast(
                origin: collision.GetCliffCheckPoint().position,
                direction: Vector2.down,
                distance: 10f,
                layerMask: collision.GetGroundLayer());

            float cliffHeight = hit.point.y - collision.GetCliffCheckPoint().position.y;

            return cliffHeight > CliffHeightThreshold;
        }
        return false;
    }
}
