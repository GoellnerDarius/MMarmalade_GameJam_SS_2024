using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Input playerInput;
    CharacterController characterController;

    Vector2 currentMovementInput;
    Vector3 currentMovement;

    bool isMovementPressed;
    float rotationFactor = 10.0f;
    float playerSpeed = 5.0f;

    void Awake()
    {
        playerInput = new Input();
        characterController = GetComponent<CharacterController>();

/*
        //Move Input Pressed callback
        playerInput.CharacterControls.Move.started += OnMovementInput;
        
        //Move Input canceled callback
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
        
        //Move Input finished callback
        playerInput.CharacterControls.Move.performed += OnMovementInput;*/
    }

    public void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void HandleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;
        if(isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactor);
        }
    }

    void Update()
    {
        HandleRotation();
        characterController.Move(currentMovement * playerSpeed * Time.deltaTime);
    }

    void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
}
