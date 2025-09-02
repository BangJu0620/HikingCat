using UnityEngine;

public class PlayerJumpUpState : IPlayerState
{
    public Player player { get; set; }
    public void Init(Player player)
    {
        this.player = player;
    }
    public void EnterState() => player.anim.PlayAnimation(PlayerAnimationState.JumpUp);
    public void ExitState() { }
    public void ReEnterState() { }

    public void UpdateState()
    {
        if (player.body.Velocity.y <= 0)
            player.stateMachine.ChangeState<PlayerFallDownState>();

        player.body.InputHandler(player.controller.CurMovementInput * player.status.Speed);
    }

    public void InputHandler<T>(InputType type, T data)
    {
    }
}
