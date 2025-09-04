using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Inspector ���� ������Ʈ")]
    [SerializeField] private StatusModel statusModel;
    [SerializeField] private PlayerStateMachine stateMachine;
    [SerializeField] private AnimationHandler anim;

    public Vector2 CurMovementInput { get; private set; }
    public bool IsJumpKeyHeld { get; private set; }
    public bool IsMoveKeyHeld { get; private set; }

    private void Update()
    {
        // ���� ������Ʈ�� ȣ��
        stateMachine.UpdateState();
        statusModel.SetTargetVelocity(CurMovementInput);

/*
        if (Input.GetKeyDown(KeyCode.G))
        {
            Leaderboard.CreateNewScore("test", Time.time, out LeaderboardData data);
            if(!data.Equals(default(LeaderboardData)))
            {
                _ = Leaderboard.RegisterScore(data);
            }
        }*/
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        if (context.phase == InputActionPhase.Performed)
        {
            CurMovementInput = input;
            IsMoveKeyHeld = true;
            stateMachine.OnInput(InputType.Move);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            CurMovementInput = Vector2.zero;
            IsMoveKeyHeld = false;
            stateMachine.OnInput(InputType.StopMove);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            IsJumpKeyHeld = true;
            stateMachine.OnInput(InputType.JumpPressed);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            IsJumpKeyHeld = false;
            stateMachine.OnInput(InputType.JumpReleased);
        }
    }

    /*
        [Header("����")]
        [SerializeField] private bool isJumpCharge = false;
        [SerializeField] private bool isJumpChargeKeyInput = false;
        [SerializeField] private float jumpChargeTime = 0f;
        [SerializeField] private float maxChargeTime = 1.2f;

        [Header("�̵�")]
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

            // �̵� ���� �� �̵� velocity ����
            if (isMoving)
            {
                curVelocity = Vector2.Lerp(curVelocity, targetVelocity, accelRate * Time.deltaTime);
            }
            else
            {
                curVelocity = Vector2.Lerp(curVelocity, Vector2.zero, deAccelRate * Time.deltaTime);
            }

            // �Է� Ű �̵� ���� �ƴϰų� �ӵ��� ��������, �̵� ���� ó��
            if(Mathf.Abs(curVelocity.x) <= 0.05f && !isMovekeyInput)
            {
                isMoving = false;
                curVelocity.x = 0;
                // �̵� ���� �ƴ� �� ���� �ְ�, ���� ��¡ Ű �Է� �� ��¡ ����
                if (isJumpChargeKeyInput && !isJumpCharge && physicsModel.isGround)
                {
                    isJumpCharge = true;
                    stateMachine.ChangeState<PlayerChargeState>();
                    jumpChargeTime = Time.time;
                }
            }
            else
            {
                // �̵� ���� �� �����𵨿� �ӷ� �Է�
                isMoving = true;
                physicsModel.InputHandler(curVelocity);
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            // ��¡ �� �̵� �Ұ�
            if (!isJumpCharge)
            {
                // Ű �Է� �� �̵� ó��
                if (context.phase == InputActionPhase.Performed)
                {
                    isMoving = true;
                    isMovekeyInput = true;
                    curMovementInput = context.ReadValue<Vector2>();
                }
                // Ű �Է� ���� �� ���� ó��
                else if (context.phase == InputActionPhase.Canceled)
                {
                    isMovekeyInput = false;
                    curMovementInput = Vector2.zero;
                }
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            // ���⼭ ���� ��¡ ���õ� ���� �ؾ���.
            if (!isJumpChargeKeyInput && context.phase == InputActionPhase.Performed)
            {
                isJumpChargeKeyInput = true;
            }
            if (context.phase == InputActionPhase.Canceled)
            {
                // ��¡ Ű ���� �� �ڵ鷯 ���� (����)
                isJumpChargeKeyInput = false;
                if (isJumpCharge) isJumpCharge = false;
                stateMachine.OnInput<Null>(InputType.JumpReleased, null);
            }
        }*/
}
