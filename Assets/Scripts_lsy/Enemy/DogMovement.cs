using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMovement : CharacterComponents_lsy
{
    //[SerializeField] private float jumpHeight = 8f;
    [SerializeField] private float walkSpeed = 1f;
    public float speed { get; set; }
   // public float jumpForce { get; set; }

    public bool faceRight = true;
   // public bool isJump;

    public Transform leftPoint, rightPoint;
   // public Transform groundCheck;
   // public LayerMask ground;

    private float leftx, rightx;
   // private int jumpCount;
    //private bool jumpPressed;
    // Internal
    private readonly int movingParamater = Animator.StringToHash("Moving");

    protected override void Start()
    {
        base.Start();
        speed = walkSpeed;
        transform.DetachChildren();
        leftx = leftPoint.position.x;
        rightx = rightPoint.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
        SetHorizontal(1f);
        //jumpForce = jumpHeight;
    }

    /*protected override void HandleInput()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }
    }*/

    protected override void HandleAbility()
    {
        base.HandleAbility();
        UpdateAnimations();

    }


    protected void FixedUpdate()
    {
        //GroundCheck();
        
        MoveCharacter();
        
        //Jump();
    }

    /*public void GroundCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
    }*/

    private void MoveCharacter()
    {
        float movementSpeed = horizontalInput * speed;
        Vector2 move = new Vector2(movementSpeed, 0f);
        controller.SetMovement(move);
        if (horizontalInput == 1)
        {
            if (transform.position.x > rightx)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                faceRight = false;
                horizontalInput = -1;
            }
        }
        else
        {
            if (transform.position.x < leftx)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                faceRight = true;
                horizontalInput = 1;
            }
        }

    }

    /*private void Jump()
    {
        if (isGround)
        {
            jumpCount = 2;
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
    }*/

    void UpdateAnimations()
    {
        if (character.CharacterAnimator != null)
        {
            if (horizontalInput == 0.0f)
            {
                character.CharacterAnimator.SetBool(movingParamater, value: false);
                
                }
            else
            {
                   character.CharacterAnimator.SetBool(movingParamater, value: true);
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
