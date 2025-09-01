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

    [Header("점프")]
    [SerializeField] private bool isJumpCharge = false;
    [SerializeField] private bool isJumpChargeKeyInput = false;
    [SerializeField] private float jumpChargeTime = 0f;
    [SerializeField] private float maxChargeTime = 1.2f;

    [Header("이동")]
    [SerializeField] private float accelTime = 0.2f;
    [SerializeField] private float deAccelTime = 0.1f;

    public Vector2 curMovementInput;
    private Vector2 curVelocity;
    private Vector2 targetVelocity;

    private float accelRate;
    private float deAccelRate;

    private bool isMoving = false;

    private void Awake()
    {
        accelRate = 1f / accelTime;
        deAccelRate = 1f / deAccelTime;
    }

    private void Update()
    {
        targetVelocity = curMovementInput * statusModel.Speed;

        if (isMoving)
        {
            curVelocity = Vector2.Lerp(curVelocity, targetVelocity, accelRate * Time.deltaTime);
        }
        else
        {
            curVelocity = Vector2.Lerp(curVelocity, Vector2.zero, deAccelRate * Time.deltaTime);
        }

        if(Mathf.Abs(curVelocity.x) <= 0.05f)
        {
            isMoving = false;
            curVelocity.x = 0;
            if (isJumpChargeKeyInput && !isJumpCharge && physicsModel.isGround)
            {
                isJumpCharge = true;
                jumpChargeTime = Time.time;
            }
        }
        else
        {
            isMoving = true;
            physicsModel.InputHandler(curVelocity);
        }
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
                curMovementInput = Vector2.zero;
            }
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // 여기서 점프 차징 관련도 들어가긴 해야함.
        if (!isJumpChargeKeyInput && context.phase == InputActionPhase.Performed)
        {
            isJumpChargeKeyInput = true;
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            isJumpChargeKeyInput = false;
            if (isJumpCharge)
            {
                isJumpCharge = false;

                float heldTime = Time.time - jumpChargeTime;

                // 0~1로 정규화
                float chargeRange = Mathf.Clamp01(heldTime / maxChargeTime);
                Debug.Log($"Time : {Time.time} jumpChargeT : {jumpChargeTime} heltTime : {heldTime} chargeRange : {chargeRange}");
                // 점프 실행
                physicsModel.Jump(statusModel.GetJumpForce(chargeRange));
            }
        }
    }
}
