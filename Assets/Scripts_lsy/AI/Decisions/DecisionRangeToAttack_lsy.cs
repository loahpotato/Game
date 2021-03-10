using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Detect Range To Attack", fileName = "DecisionRangeToAttack")]
public class DecisionRangeToAttack_lsy : AIDecision_lsy
{
    public float minDistanceToAttack = 1.5f;

    public override bool Decide(StateController_lsy controller)
    {
        return PlayerInRangeToAttack(controller);
    }

    private bool PlayerInRangeToAttack(StateController_lsy controller)
    {
        if (controller.Target != null)
        {
            // Get Distance
            float distanceToAttack = (controller.Target.position - controller.transform.position).sqrMagnitude;

            // Compare and return if we are close to the target
            return distanceToAttack < Mathf.Pow(minDistanceToAttack, 2);
        }

        return false;
    }
}
