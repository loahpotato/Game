using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UIElements;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Patrol", fileName = "ActionPatrol")]
public class ActionPatrol_lsy : AIAction_lsy
{
    private Vector2 newDirection;

    public override void Act(StateController_lsy controller)
    {
        Patrol(controller);
    }

    private void Patrol(StateController_lsy controller)
    {
        newDirection = controller.Path.CurrentPoint - controller.transform.position;
        newDirection = newDirection.normalized;

        controller.CharacterMovement.SetHorizontal(newDirection.x);
        controller.CharacterMovement.SetVertical(newDirection.y);
    }
}
