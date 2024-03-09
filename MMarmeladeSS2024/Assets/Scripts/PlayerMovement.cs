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

    //Dash
    public bool isDashing = false;
    [SerializeField]
    float dashSpeed = 30.0f;
    [SerializeField]
    float dashTime = 0.1f;

    //Knockback
    public bool isKnockedBack = false;
    [SerializeField]
    float knockbackSpeed = 30.0f;
    [SerializeField]
    float knockbackTime = 0.05f;

    public Material damageMaterial;
    public Material normalMaterial;

    float damageIndicatorTimer = 1.0f;

    void Awake()
    {
        playerInput = new Input();
        characterController = GetComponent<CharacterController>();
    }

    public void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    private IEnumerator DashCoroutine()
    {
        isDashing = true;
        float startTime = Time.time;
        Vector3 dashDirection = transform.TransformDirection(Vector3.forward);
        
        while (Time.time < startTime + dashTime && isDashing)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dashDirection, out hit, dashSpeed * Time.deltaTime))
            {
                if (hit.collider.CompareTag("Wall"))
                {
                    Debug.Log("Collided with a wall");
                    isDashing = false;
                    StartCoroutine(KnockbackCoroutine());
                    break;
                }
            }
            characterController.Move(transform.forward * dashSpeed * Time.deltaTime);
            yield return null;
        }

        isDashing = false;
    }

    public IEnumerator KnockbackCoroutine()
    {
        Debug.Log("Apply Knockback");
        isKnockedBack = true;
        float startTime = Time.time;
        while (Time.time < startTime + knockbackTime)
        {
            characterController.Move(transform.forward * (-1) * knockbackSpeed * Time.deltaTime);
            //transform.Translate(Vector3.forward * knockbackSpeed * Time.deltaTime);
            yield return null;
        }

        isKnockedBack = false;
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

    public void ReceiveDamage()
    {
        gameObject.GetComponent<Renderer>().material = damageMaterial;
        StartCoroutine(DamageReceivedCoroutine());
    }

    private IEnumerator DamageReceivedCoroutine()
    {
        float startTime = Time.time;
        
        while (Time.time < startTime + damageIndicatorTimer)
        {
            yield return null;
        }

        gameObject.GetComponent<Renderer>().material = normalMaterial;
    }

    void Update()
    {
        if(!isDashing && !isKnockedBack)
        {
            HandleRotation();
            characterController.Move(currentMovement * playerSpeed * Time.deltaTime);
        }
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
