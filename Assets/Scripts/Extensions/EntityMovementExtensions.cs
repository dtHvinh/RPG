using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public static class EntityMovementExtensions
{
    public static void SetVelocityX(this Player player, float x)
    {
        player.SetVelocity(x, 0);
    }

    public static void SetVelocityY(this Player player, float y)
    {
        player.SetVelocity(0, y);
    }

    public static void OnlyMoveX(this Player player)
    {
        player.SetVelocity(player.Rb.linearVelocityX, 0);
    }

    public static void OnlyMoveY(this Player player)
    {
        player.SetVelocity(0, player.Rb.linearVelocityY);
    }

    /// <summary>
    /// Move character with its base speed stat in the given direction.
    /// </summary>
    /// <param name="direction">The direction retrieve by getting the sign, value is emit</param>
    public static void MoveWithBaseSpeed(this Entity entity, float direction)
    {
        entity.SetVelocity(entity.Stats.GetMoveSpeed() * Mathf.Sign(direction), entity.Rb.linearVelocityY);
    }

    public static void MoveWithBaseSpeed(this Entity entity)
    {
        entity.SetVelocity(entity.Stats.GetMoveSpeed() * entity.FacingDirection, entity.Rb.linearVelocityY);
    }

    public static bool IsHorizontallyMoving(this Entity entity) => entity.Rb.linearVelocity.x != 0;
}
