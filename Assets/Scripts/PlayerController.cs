using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private bool Grounded;
    private Rigidbody rb;
    public float moveSpeed;
    public float mouseSensitivity = 2f;
    private Vector2 rotationInput;
    Vector2 movementInput;
    Vector3 movementDirection;
    public Camera cam;
    public float rotationFactor = 1.0f;
    public bool yRotationInvert = true;
    [HideInInspector] public Vector2 rotation = Vector2.zero;
    private float currentCameraRotationX = 0.0f;
    public float minCameraRotationX = -90.0f;
    public float maxCameraRotationX = 90.0f;


    void Start()
    {
        Grounded = true;
        rb = GetComponent<Rigidbody>(); 
    }

    private void Update()
    {
        FirstPersonRotation();

    }

    private void FixedUpdate()
    {
        FirstPersonMovement();
    }

    private void FirstPersonRotation()
    {
        Vector3 camRot = cam.transform.localEulerAngles;

        currentCameraRotationX += (yRotationInvert ? -1.0f : 1.0f) * rotationInput.y * rotationFactor;

        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, minCameraRotationX, maxCameraRotationX);

        camRot.x = currentCameraRotationX;
        cam.transform.localRotation = Quaternion.Euler(camRot);

        Vector3 playerRot = transform.eulerAngles;
        playerRot.y += rotationInput.x * rotationFactor;
        transform.rotation = Quaternion.Euler(playerRot);
    }

    private void FirstPersonMovement()
    {
        Vector3 movement = (transform.forward * movementInput.y) + (transform.right * movementInput.x);
        movement *= moveSpeed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

    }

    public void OnLook(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<Vector2>() * mouseSensitivity;
    }

    public void OnJump (InputAction.CallbackContext context)
    {
        if(context.performed && Grounded == true)
        {
            Grounded = false;
            rb.velocity = new Vector3(0, rb.velocity.y, 0);

            Debug.Log("Jumping");

            rb.AddForce(0, 2000, 0);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Grounded");

            Grounded = true;
        }
    }
}
