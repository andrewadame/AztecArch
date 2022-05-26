using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{

    public IdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        var input = new Vector2(Input.GetAxisRaw("Input1X"), Input.GetAxisRaw("Input1Y"));
        if (input != Vector2.zero)
            this.stateMachine.ChangeState(player.moveState);
    }

}
