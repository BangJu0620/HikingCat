using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    void Interact();
}

public class Interaction : MonoBehaviour
{
    public IInteractable curInteractable;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            curInteractable.Interact();
        }
    }
}
