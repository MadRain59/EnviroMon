using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

//[RequiredComponent(typeof(Rigidbody2D) for calling RigidBody2D, can be used but unnecessary
[RequireComponent(typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    TouchingDirections grounded;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float airWalkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    public float jumpImpulse = 10f;
    //calls Vector2
    Vector2 moveInput;

    //checks for CurrentMoveSpeed
    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !grounded.IsOnWall)
                {
                    if (grounded.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            //if IsRunning = true then return runSpeed (Basically if IsRunning is true then run speed activate)
                            return runSpeed;
                        }
                        //if IsRunning = false then returns to walkSpeed
                        else
                        {
                            return walkSpeed;
                        }
                    }

                    else
                    {
                        //Air Move
                        return airWalkSpeed;
                    }
                }
                //if both IsMoving and IsRunning is false, return to Idle which is return 0;
                else
                {
                    return 0;
                }

            }
            else
            {
                return 0;
            }
        }
    }



    [SerializeField] private bool _isMoving = false;
    public bool IsMoving
    {
        //get isMoving value basically, reading whether or not IsMoving is true or false
        get
        {
            return _isMoving;
        }

        private set
        {
            _isMoving = value;
            anim.SetBool("IsMoving", value);
        }
    }

    [SerializeField] private bool _isRunning = false;
    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }

        private set
        {
            _isRunning = value;
            anim.SetBool("IsRunning", value);
        }
    }

    public bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get
        {
            //checks if isFacingRight already
            return _isFacingRight;
        }
        private set
        {
            //checks if isFacingRight is not true then flips the localscale
            if(_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            //shows the current value of isFacingRight
            _isFacingRight = value;
        }
    }

    public bool CanMove 
    {
        get
        {
            return anim.GetBool("canMove");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        grounded = GetComponent<TouchingDirections>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        //tells rb the x and y position of the character, x is multiplied by the walk speed
        //y whill most likely be multiplied by the jumforce
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        anim.SetFloat("yVelocity", rb.velocity.y);
    }
    //parameter to read the value of the context which in this case is Vector2.
    public void OnMove(InputAction.CallbackContext context)
    {
        //moveInput is equal to the current value of Vector2
        moveInput = context.ReadValue<Vector2>();
        //decides whether IsMoving is true or false
        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
    }

    public void SetFacingDirection(Vector2 moveInput)
    {
        //checks whether or not you are already facing right, if not facing right then will not flip
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        //Check if alive as well
        // && grounded.IsGrounded && CanMove put this in later, can't use because bugged
        if (context.started)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
            anim.SetTrigger("IsJumping");
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            anim.SetTrigger("attack");
        }
    }
}
