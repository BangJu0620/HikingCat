using UnityEngine;

public class PlayerJumpUpState : IPlayerState
{
    public Player player { get; set; }
    public void Init(Player player)
    {
        this.player = player;
    }
    public void EnterState() 
    {
        player.body.OnWallHitAction += OnWallHitEvent;
        player.anim.PlayAnimation(PlayerAnimationState.JumpUp);
    }

    public void ExitState()
    {
        player.body.OnWallHitAction -= OnWallHitEvent;
    }
    public void ReEnterState() { }

    public void UpdateState()
    {
        if (player.body.Velocity.y <= 0)
            player.stateMachine.ChangeState<PlayerFallDownState>();

        player.status.CurVelocity =
            Vector2.Lerp(player.status.CurVelocity, player.status.TargetVelocity,
            player.status.AccelRate * Time.deltaTime);
        player.body.InputHandler(player.status.CurVelocity);
    }

    public void InputHandle(InputType type) { }

    public void OnWallHitEvent(Vector2 normalVec)
    {
        // 벽에 어떻게든 충돌하면 소리 재생
        if (player.footStep != null)
        {
            player.footStep.PlayWallHitSFX();
        }

        // 아래쪽 벡터 충돌 (머리 위로 맞음)
        if(normalVec.y < -0.05f)
        {
            GameManager.Instance.gameData.headHitCount++;
        }
        // 가로 벽 충돌 횟수
        else if(Mathf.Abs(normalVec.x) >= 0.995f)
        {
            GameManager.Instance.gameData.verticalWallHitCount++;
        }
    }
}
