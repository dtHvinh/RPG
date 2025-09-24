using System.Collections;
using UnityEngine;

public class Player_BasicAttack1State : Player_BasicAttackState
{
    public Player_BasicAttack1State(EntityStateMachine stateMachine, Player player, string animationBoolName)
        : base(stateMachine, player, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Animator.SetInteger(BASIC_ATTACK_INDEX, BasicAttackConstants.PLAYER_BASIC_ATTACK_1_INDEX);

        ApplyAttackVelocity(player.AttackVelocity[BasicAttackConstants.PLAYER_BASIC_ATTACK_1_INDEX - 1]);
    }

    public override void Update()
    {
        base.Update();

        if (player.Inputs.Player.Attack.WasPressedThisFrame())
        {
            QueueNextAttack(player.BasicAttack2State);
        }
    }
}
