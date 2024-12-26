using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    PlayerInputControls pc;

    [Header("Physics")]
    [SerializeField] Vector3 moveDirection;
    [SerializeField] Vector3 verticalVelocity;

    [SerializeField] float[] moveSpeeds;
    float moveSpeed;
    [SerializeField] float gravity = -30f;
    [SerializeField] float gravityScale;
    [SerializeField] float jumpHeight;

    [SerializeField] Transform cameraTransform;

    [SerializeField] LayerMask groundMask;

    [SerializeField] bool isGrounded = true;
    [SerializeField] bool isJumping = false;
    [SerializeField] bool isCrouching = false;

    [SerializeField] CharacterController cc;

    [SerializeField] float crouchHeight;
    [SerializeField] float standingHeight;


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

        if(isCrouching)
        {
            moveSpeed = moveSpeeds[1];
            transform.localScale = new Vector3(1.25f, crouchHeight, 1.25f);
        }
        else
        {
            moveSpeed = moveSpeeds[0];
            transform.localScale = new Vector3(1.25f, standingHeight, 1.25f);
        }

        //Apply movement to cc
        Vector3 moveVector = transform.right * moveDirection.x + transform.forward * moveDirection.z;
        float magnitude = moveVector.magnitude;

        if (magnitude > 0 && !UI_Manager.instance.GetInventoryState())
        {
            cc.Move((moveVector / magnitude) * moveSpeed * Time.deltaTime);
        }
        cc.Move(verticalVelocity * Time.deltaTime);
        
    }

    void ApplyGravity()
    {
        if (!isGrounded)
        {
            if(verticalVelocity.y < 0)
                verticalVelocity.y += gravity * Time.deltaTime*gravityScale;
            else
                verticalVelocity.y += gravity * Time.deltaTime;
        }
    }

    //Links the input values to their corresponding variables
    void LinkInputsToVariables()
    {
        if (pc != null)
        {
            moveDirection = pc.move.ReadValue<Vector3>();
        }
        else
        {
            Debug.LogWarning("Player_Controls component is missing.");
        }
    }

    void CheckIfGrounded()
    {
        isGrounded = cc.isGrounded;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        isJumping = true;
    }
    public void Crouch(InputAction.CallbackContext context)
    {
        isCrouching = !isCrouching;
    }

    //Initialises all of the reference variables at the start.
    void InitialiseVariables()
    {
        cc = GetComponent<CharacterController>();
        pc = GetComponent<PlayerInputControls>();
    }
}
