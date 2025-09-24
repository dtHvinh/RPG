using System.Collections;
using UnityEngine;

public class Player_BasicAttack2State : Player_BasicAttackState
{
    public Player_BasicAttack2State(EntityStateMachine stateMachine, Player player, string animationBoolName) 
        : base(stateMachine, player, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Animator.SetInteger(BASIC_ATTACK_INDEX, BasicAttackConstants.PLAYER_BASIC_ATTACK_2_INDEX);

        ApplyAttackVelocity(player.AttackVelocity[BasicAttackConstants.PLAYER_BASIC_ATTACK_2_INDEX - 1]);
    }

    public override void Update()
    {
        base.Update();

        if (player.Inputs.Player.Attack.WasPressedThisFrame())
        {
            QueueNextAttack(player.BasicAttack3State);
        }
    }
}
