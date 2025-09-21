
using UnityEngine;

public class Player_AiredState : PlayerState
{
    public Player_AiredState(EntityStateMachine stateMachine, Player player, string animationBoolName) : base(stateMachine, player, animationBoolName)
    {
    }


    public override void Update()
    {
        base.Update();

        if (MoveInput.x != 0)
            player.SetVelocity(MoveInput.x * Stats.GetMoveSpeed() * player.JumpAirResistance, Rb.linearVelocityY);

        if(Inputs.Player.Attack.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.PlungeAttackState);
        }
    }
}
