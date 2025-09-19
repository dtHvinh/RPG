using UnityEngine;

public class Player_BasicAttack3State: Player_BasicAttackState
{
    public Player_BasicAttack3State(EntityStateMachine stateMachine, Player player, string animationBoolName) 
        : base(stateMachine, player, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Animator.SetInteger(BASIC_ATTACK_INDEX, BasicAttackConstants.PLAYER_BASIC_ATTACK_3_INDEX);

        ApplyAttackVelocity(player.AttackVelocity[BasicAttackConstants.PLAYER_BASIC_ATTACK_3_INDEX - 1]);
    }
}
