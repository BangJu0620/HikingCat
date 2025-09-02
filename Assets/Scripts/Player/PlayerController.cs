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
    [SerializeField] private PlayerStateMachine stateMachine;


    public Vector2 CurMovementInput { get; private set; }
    public bool IsJumpKeyHeld { get; private set; }

    private void Update()
    {
        // 상태 업데이트만 호출
        stateMachine.UpdateState();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        if (context.phase == InputActionPhase.Performed)
        {
            CurMovementInput = input;
            stateMachine.OnInput(InputType.Move, input);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            CurMovementInput = Vector2.zero;
            stateMachine.OnInput<Null>(InputType.StopMove, null);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            IsJumpKeyHeld = true;
            stateMachine.OnInput<Null>(InputType.JumpPressed, null);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            IsJumpKeyHeld = false;
            stateMachine.OnInput<Null>(InputType.JumpReleased, null);
        }
    }

    /*
        [Header("점프")]
        [SerializeField] private bool isJumpCharge = false;
        [SerializeField] private bool isJumpChargeKeyInput = false;
        [SerializeField] private float jumpChargeTime = 0f;
        [SerializeField] private float maxChargeTime = 1.2f;

        [Header("이동")]
        [SerializeField] private float accelTime = 0.2f;
        [SerializeField] private float deAccelTime = 0.1f;
        private bool isMovekeyInput = false;

        public Vector2 curMovementInput;
        private Vector2 curVelocity;
        private Vector2 targetVelocity;

        private float accelRate;
        private float deAccelRate;

        [SerializeField] private bool isMoving = false;

        private void Awake()
        {
            accelRate = 1f / accelTime;
            deAccelRate = 1f / deAccelTime;
        }

        private void Update()
        {
            stateMachine.UpdateState();
            targetVelocity = curMovementInput * statusModel.Speed;

            // 이동 중일 때 이동 velocity 조절
            if (isMoving)
            {
                curVelocity = Vector2.Lerp(curVelocity, targetVelocity, accelRate * Time.deltaTime);
            }
            else
            {
                curVelocity = Vector2.Lerp(curVelocity, Vector2.zero, deAccelRate * Time.deltaTime);
            }

            // 입력 키 이동 중이 아니거나 속도가 낮아지면, 이동 멈춤 처리
            if(Mathf.Abs(curVelocity.x) <= 0.05f && !isMovekeyInput)
            {
                isMoving = false;
                curVelocity.x = 0;
                // 이동 중이 아닐 때 땅에 있고, 점프 차징 키 입력 시 차징 시작
                if (isJumpChargeKeyInput && !isJumpCharge && physicsModel.isGround)
                {
                    isJumpCharge = true;
                    stateMachine.ChangeState<PlayerChargeState>();
                    jumpChargeTime = Time.time;
                }
            }
            else
            {
                // 이동 중일 때 물리모델에 속력 입력
                isMoving = true;
                physicsModel.InputHandler(curVelocity);
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            // 차징 시 이동 불가
            if (!isJumpCharge)
            {
                // 키 입력 시 이동 처리
                if (context.phase == InputActionPhase.Performed)
                {
                    isMoving = true;
                    isMovekeyInput = true;
                    curMovementInput = context.ReadValue<Vector2>();
                }
                // 키 입력 해제 시 멈춤 처리
                else if (context.phase == InputActionPhase.Canceled)
                {
                    isMovekeyInput = false;
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
                // 차징 키 종료 시 핸들러 전달 (점프)
                isJumpChargeKeyInput = false;
                if (isJumpCharge) isJumpCharge = false;
                stateMachine.OnInput<Null>(InputType.JumpReleased, null);
            }
        }*/
}
