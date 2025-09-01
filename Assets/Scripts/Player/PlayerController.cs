using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Inspector ���� ������Ʈ")]
    [SerializeField] private KinematicObj physicsModel;
    [SerializeField] private StatusModel statusModel;

    [Header("����")]
    public bool isJumpCharge = false;
    private float jumpChargeTime = 0f;
    [SerializeField] private float maxChargeTime = 1.2f;

    [Header("�̵�")]
    [SerializeField] private float accelTime = 0.2f;
    [SerializeField] private float deAccelTime = 0.2f;

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

        physicsModel.InputHandler(curVelocity);
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
            // ���⼭ ���� ��¡ ���õ� ���� �ؾ���.
            if (context.phase == InputActionPhase.Started)
            {
                isJumpCharge = true;
                jumpChargeTime = Time.time;
            }
            if (context.phase == InputActionPhase.Canceled)
            {
                isJumpCharge = false;

                float heldTime = Time.time - jumpChargeTime;

                // 0~1�� ����ȭ
                float chargeRange = Mathf.Clamp01(heldTime / maxChargeTime);

                // ���� ����
                physicsModel.Jump(statusModel.GetJumpForce(chargeRange));
            }
        }
    }
}
