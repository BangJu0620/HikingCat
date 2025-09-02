using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChargeState : IPlayerState
{
    private float jumpChargeT;

    public Player player { get; set; }
    public void Init(Player player)
    {
        this.player = player;
    }

    public void EnterState()
    {
        jumpChargeT = Time.time;
        player.anim.PlayAnimation(PlayerAnimationState.Charge);
    }

    public void ExitState()
    {
        float heldTime = Time.time - jumpChargeT;
        float chargeRange = Mathf.Clamp01(heldTime / player.status.MaxChargeTime);

        player.body.Jump(player.status.GetJumpForce(chargeRange));

    }


    public void ReEnterState()
    {
    }

    public void UpdateState()
    {
    }

    public void InputHandler<T>(InputType type, T data)
    {
        if (type == InputType.JumpReleased)
        {
            player.stateMachine.ChangeState<PlayerJumpUpState>();
        }
    }
}
