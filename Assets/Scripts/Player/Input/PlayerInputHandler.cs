using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Movement Settings")]
    private Vector2 movementInput; 

    //[Header("Jump Settings")]
    

    //[Header("Dash Settings")]

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }

        if (context.performed)
        {

        }

        if (context.canceled)
        {

        }
    }
}
