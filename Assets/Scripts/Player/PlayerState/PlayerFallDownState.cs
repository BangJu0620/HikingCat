using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallDownState : IPlayerState
{
    public Player player { get; set; }
    public void Init(Player player)
    {
        this.player = player;
    }
    public void EnterState() => player.anim.PlayAnimation(PlayerAnimationState.FallDown);
    public void ExitState() { }
    public void ReEnterState() { }

    public void UpdateState()
    {
        if (player.body.isGround)
            player.stateMachine.ChangeState<PlayerLandingState>();

        player.body.InputHandler(player.controller.CurMovementInput * player.status.Speed);
    }

    public void InputHandler<T>(InputType type, T data) { }
}
