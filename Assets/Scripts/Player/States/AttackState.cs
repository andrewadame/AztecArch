using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : PlayerState
{

    public AttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {

    }

    public override void AnimationFinishTrigger()
    {
        this.stateMachine.ChangeState(player.idleState);
    }

}
