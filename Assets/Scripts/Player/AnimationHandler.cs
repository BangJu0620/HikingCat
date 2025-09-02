using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAnimationState
{
    Idle,
    Move,
    Charge,
    JumpUp,
    FallDown,
    Landing,
}

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void PlayAnimation(PlayerAnimationState state)
    {
        switch (state)
        {
            case PlayerAnimationState.Idle:
                break;
            case PlayerAnimationState.Move:
                break;
            case PlayerAnimationState.Charge:
                break;
            case PlayerAnimationState.JumpUp:
                break;
            case PlayerAnimationState.FallDown:
                break;
            case PlayerAnimationState.Landing:
                break;
        }
    }
}
