using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float walkingSpeed = 1f;
    [SerializeField] private float runningSpeed = 3f;
    [SerializeField] private float jumpSpeed = 4.0f;
    [SerializeField] private float gravity = 20.0f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float mouseSensivity = 2.0f;
    [SerializeField] private float lookXLimit = 45.0f;
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    [HideInInspector]
    public float velocity = 0;


    [HideInInspector]
    public UnityEvent OnMove;

    [HideInInspector]
    public bool CanMove = true;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mouseSensivity = Setting_Data.sensitivity_Index;
    }

    private void Update()
    {
        if (CanMove)
        {
            Move();
            RotateByMouse();
        }


    }

    private void Move()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedZ =  Input.GetAxis("Vertical");
        float curSpeedX = Input.GetAxis("Horizontal");
        float movementDirectionY = moveDirection.y;

        float speed = ((velocity > 0 && curSpeedZ > 0) ||(velocity < 0 && curSpeedZ < 0)) ? Mathf.Lerp(walkingSpeed, runningSpeed, Mathf.Abs(velocity)) : walkingSpeed;

        moveDirection = ((forward * curSpeedZ) + (right * curSpeedX)).normalized * speed;

        if (Input.GetButton("Jump") && characterController.isGrounded)
            moveDirection.y = jumpSpeed;
        else
            moveDirection.y = movementDirectionY;

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        CulculateVelocity(curSpeedZ, isRunning);

        OnMove.Invoke();

    }


    private void CulculateVelocity(float curSpeedZ, bool isRunning)
    {
        float maxValue = isRunning ? 1 : 0.1f;
        float minValue = isRunning ? -1 : -0.1f;


        if (curSpeedZ > 0 && velocity < maxValue)
            velocity += 0.7f * Time.deltaTime;
        else if (curSpeedZ == 0 && velocity > 0.05)
            velocity -= 1.4f * Time.deltaTime;
        /*else if (curSpeedZ == 0 && velocity < 0.05)
            velocity = 0;*/


        if (curSpeedZ < 0 && velocity > minValue)
            velocity -= 0.7f * Time.deltaTime;
        else if (curSpeedZ == 0 && velocity < -0.05)
            velocity += 1.4f * Time.deltaTime;
        /*else if (curSpeedZ == 0 && velocity > -0.05)
            velocity = 0;*/

        velocity = Mathf.Clamp(velocity, -1, 1);
        //print(velocity);
    }
    private void RotateByMouse()
    {
        // Player and Camera rotation
        rotationX += -Input.GetAxis("Mouse Y") * mouseSensivity;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * mouseSensivity, 0);
    }
}
