using UnityEngine;

public static class PlayerMovementExtensions
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
}
