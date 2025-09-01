using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private KinematicObj physicsModel;
    [SerializeField] private StatusModel statusModel;
    public Vector2 curMovementInput;

    private void Update()
    {
        physicsModel.InputHandler(curMovementInput * statusModel.Speed);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            physicsModel.Jump(statusModel.JumpForce);
        }
    }
}
