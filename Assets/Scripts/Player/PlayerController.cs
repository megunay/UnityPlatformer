using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Character character;

    private void Awake()
    {
        character = new Character();
    }

    private void OnEnable()
    {
        character.Enable();
    }

    private void OnDisable()
    {
        character.Disable();
    }

    private void Update()
    {
        float movementInput = character.Player.Movement.ReadValue<float>();
    }
}
