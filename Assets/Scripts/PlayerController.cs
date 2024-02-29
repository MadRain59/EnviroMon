using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

//[RequiredComponent(typeof(Rigidbody2D) for calling RigidBody2D, can be used but unnecessary
[RequireComponent(typeof(TouchingDirections), typeof(Damageable))]
public class PlayerController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    TouchingDirections touchingDirections;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float airWalkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    public float jumpImpulse = 10f;
    Damageable damageable;

    [SerializeField] private float dashSpeed = 50f; // Adjust this value based on your needs
    private bool isDashing = false;

    //calls Vector2
    Vector2 moveInput;

    //checks for CurrentMoveSpeed
    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
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
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            //shows the current value of isFacingRight
            _isFacingRight = value;
        }
    }
    //gets reference for CanMove from the animator
    public bool CanMove
    {
        get
        {
            return anim.GetBool("canMove");
        }
    }
    //gets reference for IsAlive from the animator
    public bool IsAlive
    {
        get
        {
            return anim.GetBool("IsAlive");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
    }
    private void FixedUpdate()
    {
        if (!damageable.IsHit)
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

        //checks if player IsAlive first before deciding whether or not player can move
        if (IsAlive)
        {
            //decides whether IsMoving is true or false
            IsMoving = moveInput != Vector2.zero;

            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }

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
        if (context.started && touchingDirections.IsGrounded && IsAlive)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
            anim.SetTrigger("IsJumping");
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            anim.SetTrigger("attack");

            StartCoroutine(MoveForwardDuringAttack());
        }
    }
    private IEnumerator MoveForwardDuringAttack()
    {
        // Distance to move forward during the attack animation
        float attackMoveDistance = 1.1f; // Adjust this value based on your needs

        // Cache the original position
        Vector2 originalPosition = transform.position;

        // Calculate the target position
        Vector2 targetPosition = originalPosition + new Vector2((IsFacingRight ? 1 : -1) * attackMoveDistance, 0f);

        float elapsedTime = 0f;

        while (elapsedTime < 0.1f) // Adjust the duration based on your attack animation length
        {
            // Interpolate between the original and target positions
            transform.position = Vector2.Lerp(originalPosition, targetPosition, elapsedTime / 0.5f);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }


    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            anim.SetTrigger("rangedAttack");
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    //public void OnDash(InputAction.CallbackContext context)
    //{
        //if (context.started && IsAlive)
       // {
           // StartCoroutine(Dash());
       // }
   // }
   // private IEnumerator Dash()
   // {
   //     if (!isDashing)
   //     {
   //         isDashing = true;

            // Cache the original position
            //Vector2 originalPosition = transform.position;

            // Calculate the target position
           // Vector2 targetPosition = originalPosition + new Vector2((IsFacingRight ? 1 : -1) * dashSpeed, 0f);

            //float elapsedTime = 0f;

            //while (elapsedTime < 0.15f) // Adjust the duration based on your dash animation length
            //{
                // Use Physics2D.Raycast to check for collisions in the dash path
                //RaycastHit2D hit = Physics2D.Raycast(originalPosition, (targetPosition - originalPosition).normalized, dashSpeed * elapsedTime, LayerMask.GetMask("Ground"));

                // If hit, stop the dash
                //if (hit.collider != null)
                //{
                   // break;
                //}

                // Interpolate between the original and target positions
               // transform.position = Vector2.Lerp(originalPosition, targetPosition, elapsedTime / 0 .5f);

                //elapsedTime += Time.deltaTime;

               // yield return null;
           // }

            //isDashing = false;
        //}
    //}
}
