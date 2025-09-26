using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        //stateMachine.JumpForce = stateMachine.player.Data.AirData.JumpForce;
        stateMachine.player.playerMovement.Jump(stateMachine.JumpForce);

        base.Enter();

        StartAnimation(stateMachine.player.AnimationData.jumpParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.player.AnimationData.fallParameterHash);
    }

    public override void Update()
    {
        base.Update();

        float currentSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        stateMachine.player.PlayerAnimator.SetFloat("Speed", currentSpeed);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (stateMachine.player.controller.velocity.y <= 0)
        {
            stateMachine.ChangeState(stateMachine.playerFallState);
            return;
        }
    }
}