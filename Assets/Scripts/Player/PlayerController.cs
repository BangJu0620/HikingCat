using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Inspector 연결 오브젝트")]
    [SerializeField] private KinematicObj physicsModel;
    [SerializeField] private StatusModel statusModel;
    public Vector2 curMovementInput;

    [Header("점프")]
    public bool isJumpCharge = false;
    private float jumpChargeTime = 0f;
    [SerializeField] private float maxChargeTime = 1.2f;

    private bool isMoving = false;

    private void Update()
    {
        physicsModel.InputHandler(curMovementInput * statusModel.Speed);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!isJumpCharge)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                isMoving = true;
                curMovementInput = context.ReadValue<Vector2>();
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                isMoving = false;
                curMovementInput = Vector2.zero;
            }
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!isMoving)
        {
            // 여기서 점프 차징 관련도 들어가긴 해야함.
            if (context.phase == InputActionPhase.Started)
            {
                isJumpCharge = true;
                jumpChargeTime = Time.time;
            }
            if (context.phase == InputActionPhase.Canceled)
            {
                isJumpCharge = false;

                float heldTime = Time.time - jumpChargeTime;

                // 0~1로 정규화
                float chargeRange = Mathf.Clamp01(heldTime / maxChargeTime);

                // 점프 실행
                physicsModel.Jump(statusModel.GetJumpForce(chargeRange));
            }
        }
    }
}
