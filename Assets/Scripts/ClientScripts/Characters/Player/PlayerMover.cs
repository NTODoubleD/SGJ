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
    //[SerializeField] private float speed = 5;
    [SerializeField] private float mouseSensivity = 2.0f;
    [SerializeField] private float lookXLimit = 45.0f;
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    public Camera PlayerCamera;
    [HideInInspector]
    public float velocity = 0;
    private Vector3 VelocityTest;

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
    }

    private void Update()
    {
        if (CanMove && !UIController.isOnMenu)
        {
            //Move();
            Move2();
            //print(mouseSensivity = Setting_Data.sensitivity_Index);
            RotateByMouse();
        }


    }

   /* private void Move()
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

        //print(moveDirection.x + " " + moveDirection.y + " " + moveDirection.z + " ");

        // Move the controller
        characterController.Move(moveDirection / 100f);

        CulculateVelocity(curSpeedZ, isRunning);
        OnMove.Invoke();

    }*/

    private void Move2()
    {

        bool isGrounded = characterController.isGrounded;

        if(isGrounded && VelocityTest.y < 0)
        {
            VelocityTest.y = -2f;
        }

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        
        float speed = ((velocity > 0 && z > 0) || (velocity < 0 && x < 0)) ? Mathf.Lerp(walkingSpeed, runningSpeed, Mathf.Abs(velocity)) : walkingSpeed;

        move = ((transform.forward * z) + (transform.right * x)).normalized;

        characterController.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            VelocityTest.y = Mathf.Sqrt(jumpSpeed * -2f * gravity);
        }

        VelocityTest.y += gravity * Time.deltaTime;
        //print(VelocityTest);
        characterController.Move(VelocityTest * Time.deltaTime);

        CulculateVelocity(z, isRunning);
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
        PlayerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * mouseSensivity, 0);
    }
}
