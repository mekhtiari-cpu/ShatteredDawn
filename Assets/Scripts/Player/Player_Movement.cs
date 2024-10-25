using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    Player_Controls pc;

    [Header("Physics")]
    [SerializeField] Vector3 moveDirection;
    [SerializeField] Vector3 verticalVelocity;

    [SerializeField] float moveSpeed;
    [SerializeField] float gravity = -30f;
    [SerializeField] float groundCheckRadius;
    [SerializeField] float jumpHeight;

    [SerializeField] Transform groundCheck;
    [SerializeField] Transform cameraTransform;

    [SerializeField] LayerMask groundMask;

    [SerializeField] bool isGrounded = true;
    [SerializeField] bool isJumping = false;

    [SerializeField] CharacterController cc;


    // Start is called before the first frame update
    void Start()
    {
        InitialiseVariables();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.inDebug)
        {
            return;
        }

        LinkInputsToVariables();
        ApplyMovement();
        CheckIfGrounded();
    }

    //Apply the movement from input to the rigidbody
    void ApplyMovement()
    {
        ApplyGravity();

        if(isJumping)
        {
            if(isGrounded)
            {
                verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
            }
            isJumping = false;
        }

        //Apply movement to cc
        Vector3 moveVector = transform.right * moveDirection.x + transform.forward * moveDirection.z;
        float magnitude = Mathf.Sqrt(Mathf.Pow(moveVector.x, 2) + Mathf.Pow(moveVector.z, 2)); 
        cc.Move((moveVector/magnitude) * moveSpeed * Time.deltaTime);
        cc.Move(verticalVelocity * Time.deltaTime);
    }

    void ApplyGravity()
    {
        if (!isGrounded)
        {
            verticalVelocity.y += gravity * Time.deltaTime;
        }
        else
        {
            verticalVelocity.y = 0;
        }
    }

    //Links the input values to their corresponding variables
    void LinkInputsToVariables()
    {
        moveDirection = pc.move.ReadValue<Vector3>();
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        isJumping = true;
    }

    //Initialises all of the reference variables at the start.
    void InitialiseVariables()
    {
        cc = GetComponent<CharacterController>();
        pc = GetComponent<Player_Controls>();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
