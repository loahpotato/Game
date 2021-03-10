using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDash_lsy : CharacterComponents_lsy
{
    [SerializeField] private float dashDistance = 2f;
    [SerializeField] private float dashDuration = 0.1f;

    private bool isDashing;
    private float dashTimer;
    private Vector2 dashOrigin;
    private Vector2 dashDestination;
    private Vector2 newPosition;

    protected override void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Dash();
        }
    }

    protected override void HandleAbility()
    {
        base.HandleAbility();

        if (isDashing)
        {
            if (dashTimer < dashDuration)
            {
                newPosition = Vector2.Lerp(dashOrigin, dashDestination, dashTimer / dashDuration);
                controller.SetDash(newPosition);
                dashTimer += Time.deltaTime;
            }
            else
                StopDash();
        }
    }

    private void Dash()
    {
        characterMovement.GroundCheck();
        if (characterMovement.isGround)
        {
            isDashing = true;
            dashTimer = 0f;
            controller.NormalMovement = false;
            dashOrigin = transform.position;

            Vector3 dashPosition = new Vector3(horizontalInput, 0);
            dashDestination = transform.position + dashPosition * dashDistance;
        }
    }

    private void StopDash()
    {
        isDashing = false;
        controller.NormalMovement = true;
    }
}
