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
    public void EnterState()
    {
        player.body.OnWallHitAction += OnWallHitEvent;
        player.body.OnFall += PlayerFallDataCount;
        player.anim.PlayAnimation(PlayerAnimationState.FallDown);
        isFalling = false;
    }

    public void ExitState()
    {
        player.body.OnWallHitAction -= OnWallHitEvent;
        player.body.OnFall -= PlayerFallDataCount;
    }
    public void ReEnterState() { }

    public void UpdateState()
    {
        if (player.body.isGround)
            player.stateMachine.ChangeState<PlayerLandingState>();

        player.status.CurVelocity =
            Vector2.Lerp(player.status.CurVelocity, player.status.TargetVelocity,
            player.status.AccelRate * Time.deltaTime);
        player.body.InputHandler(player.status.CurVelocity);
    }

    public void InputHandle(InputType type) { }

    public void OnWallHitEvent(Vector2 normalVec)
    {
        // ���� ��Ե� �浹�ϸ� �Ҹ� ���
        if (player.footStep != null)
        {
            player.footStep.PlayWallHitSFX();
        }

        // �Ʒ��� ���� �浹 (�Ӹ� ���� ����)
        if (normalVec.y < -0.05f)
        {
            GameManager.Instance.gameData.headHitCount++;
        }
        // ���� �� �浹 Ƚ��
        else if (Mathf.Abs(normalVec.x) >= 0.995f)
        {
            GameManager.Instance.gameData.verticalWallHitCount++;
        }
    }

    private bool isFalling = false;

    public void PlayerFallDataCount(bool isStartJump, float yDistance)
    {
        GameManager.Instance.gameData.fallDistance -= yDistance;
        if (!isStartJump)
        {
            if (!isFalling)
            {
                GameManager.Instance.gameData.deathByFootCount++;
            }
        }
        else
        {
            if (!isFalling)
            {
                GameManager.Instance.gameData.landingMissCount++;
            }
        }
        isFalling = true;
    }
}
