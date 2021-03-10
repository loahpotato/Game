using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement_lsy : CharacterComponents_lsy
{
    [SerializeField] private float jumpHeight = 8f;
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private bool doubleJump = true;
    public float speed { get; set; }
    public float jumpForce { get; set; }

    public bool isGround;
    public bool isJump;

    public Transform groundCheck;
    public LayerMask ground;

    private int jumpCount;
    private bool jumpPressed;
    // Internal
    private readonly int movingParamater = Animator.StringToHash("Moving");
    private readonly int jumpingParamater = Animator.StringToHash("Jumping");
    private readonly int fallingParamater = Animator.StringToHash("Falling");


    protected override void Start()
    {
        base.Start();
        speed = walkSpeed;
        jumpForce = jumpHeight;
    }

    protected override void HandleInput()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }
    }

    protected override void HandleAbility()
    {
        base.HandleAbility();
        UpdateAnimations();
    }


    protected void FixedUpdate()
    {
        GroundCheck();
        MoveCharacter();
        if(character.CharacterType == Character_lsy.CharacterTypes.Player)
        {
            Jump();
        }
        
    }

    public void GroundCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
    }

    private void MoveCharacter()
    {
        float movementSpeed = horizontalInput * speed;
        Vector2 move = new Vector2(movementSpeed, 0f);
        controller.SetMovement(move);


    }

    private void Jump()
    {
        if (isGround)
        {
            if(doubleJump)
                jumpCount = 2;
            else
                jumpCount = 1;
            isJump = false;
        }

        if (jumpPressed && isGround)
        {
            isJump = true;
            controller.SetJump(jumpHeight);
            jumpCount--;
            jumpPressed = false;
        }
        else if (jumpPressed && jumpCount > 0 && isJump)
        {
            controller.SetJump(jumpHeight);
            jumpCount--;
            jumpPressed = false;
        }
    }

    void UpdateAnimations()
    {
        if (character.CharacterAnimator != null)
        {
            if (isGround)
            {
                if (horizontalInput == 0.0f)
                {
                    character.CharacterAnimator.SetBool(movingParamater, value: false);
                    character.CharacterAnimator.SetBool(jumpingParamater, value: false);
                    character.CharacterAnimator.SetBool(fallingParamater, value: false);
                }
                else
                {
                    character.CharacterAnimator.SetBool(movingParamater, value: true);
                    character.CharacterAnimator.SetBool(jumpingParamater, value: false);
                    character.CharacterAnimator.SetBool(fallingParamater, value: false);
                }
            }
            else
            {
                if (controller.isFall() == false)
                {
                    character.CharacterAnimator.SetBool(movingParamater, value: false);
                    character.CharacterAnimator.SetBool(jumpingParamater, value: true);
                    character.CharacterAnimator.SetBool(fallingParamater, value: false);
                }
                else
                {
                    character.CharacterAnimator.SetBool(movingParamater, value: false);
                    character.CharacterAnimator.SetBool(jumpingParamater, value: false);
                    character.CharacterAnimator.SetBool(fallingParamater, value: true);
                }
            }
        }
    }
    public void SetHorizontal(float value)
    {
        horizontalInput = value;
    }

    public void SetVertical(float value)
    {
        verticalInput = value;
    }


    public void ResetSpeed()
    {
        speed = walkSpeed;
    }
}
