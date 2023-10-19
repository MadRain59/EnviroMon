using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    //calls Vector2
    Vector2 moveInput;

    //checks for CurrentMoveSpeed
    public float CurrentMoveSpeed
    {
        get
        {
            //checks if player IsMoving
            if (IsMoving)
            {
                //checks if player IsRunning
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
            //if both IsMoving and IsRunning is false, return to Idle which is return 0;
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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
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
    }
    //parameter to read the value of the context which in this case is Vector2.
    public void onMove(InputAction.CallbackContext context)
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
        public void onRun(InputAction.CallbackContext context)
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
}
