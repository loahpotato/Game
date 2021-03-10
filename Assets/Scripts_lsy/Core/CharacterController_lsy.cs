using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController_lsy : MonoBehaviour
{
    // Controls the current movement of this character    
    public Vector2 CurrentMovement { get; set; }

    // Returns if this character can move normally (When dashing we can't)
    public bool NormalMovement { get; set; }

    // Internal
    private Rigidbody2D myRigidbody2D;
    private Vector2 recoilMovement;

    // Start is called before the first frame update
    void Start()
    {
        NormalMovement = true;
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Recoil();

        if (NormalMovement)
        {
            MoveCharacter();
        }
    }

    private void MoveCharacter()
    {
        //float currentMovePosition = CurrentMovement * Time.fixedDeltaTime;
        //myRigidbody2D.MovePosition(currentMovePosition);
        myRigidbody2D.velocity = new Vector2(CurrentMovement.x, myRigidbody2D.velocity.y);
    }

    public void ApplyRecoil(Vector2 recoilDirection, float recoilForce)
    {
        recoilMovement = recoilDirection.normalized * recoilForce;
    }

    // Extra Move in case we need it    
    public void SetDash(Vector2 newPosition)
    {
        myRigidbody2D.MovePosition(newPosition);// = new Vector2(newPosition, myRigidbody2D.velocity.y);
    }

    // Sets the current movement of our character
    public void SetMovement(Vector2 newPosition)
    {
        CurrentMovement = newPosition;
    }

    public void SetJump(float y)
    {
        myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, y);
    }

    public bool isFall()
    {
        if (myRigidbody2D.velocity.y < 0)
            return true;
        else
            return false;
    }

    private void Recoil()
    {
        if (recoilMovement.magnitude > 0.1f)
        {
            myRigidbody2D.AddForce(recoilMovement);
        }
    }
}
