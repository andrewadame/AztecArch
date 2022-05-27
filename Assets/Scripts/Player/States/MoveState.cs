using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : PlayerState
{

    public MoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        if (Input.GetKeyDown(KeyCode.F))
            this.stateMachine.ChangeState(player.attackState);
        if (input == Vector2.zero)
            this.stateMachine.ChangeState(player.idleState);
        player.setDirection(input);
        var directionMoving = new Vector2(input.x, input.y).normalized;
        player.playerRigidBody.velocity = directionMoving * playerData.movementVelocity;
    }

}
