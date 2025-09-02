using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAnimationState
{
    Idle = 0,           // 0
    Move = 1 << 0,      // 1
    Charge = 1 << 1,    // 2
    JumpUp = 1 << 2,    // 4
    FallDown = 1 << 3,  // 8
    Landing = 1 << 4, // 16
}

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private readonly int IsMoveHash = Animator.StringToHash("IsMove");
    private readonly int IsChargeHash = Animator.StringToHash("IsCharge");
    private readonly int IsJumpUpHash = Animator.StringToHash("IsJumpUp");
    private readonly int IsFallDownHash = Animator.StringToHash("IsFallDown");

    private Dictionary<PlayerAnimationState, int> stateToHash;

    private void Awake()
    {
        stateToHash = new Dictionary<PlayerAnimationState, int>()
        {
            { PlayerAnimationState.Move, IsMoveHash },
            { PlayerAnimationState.Charge, IsChargeHash },
            { PlayerAnimationState.JumpUp, IsJumpUpHash },
            { PlayerAnimationState.FallDown, IsFallDownHash },
        };
    }

    public void PlayAnimation(PlayerAnimationState stateFlags)
    {
        if (stateToHash == null) return;
        foreach (var kvp in stateToHash)
        {
            // 해당 비트가 켜져 있는지 확인
            bool isActive = (stateFlags & kvp.Key) != 0;
            animator.SetBool(kvp.Value, isActive);
        }
    }

    public void FlipX(bool isFlip)
    {
        spriteRenderer.flipX = isFlip;
    }

}
