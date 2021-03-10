using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Detect Target", fileName = "DecisionDetect")]
public class DecisionDetect_lsy : AIDecision_lsy
{
    public float detectArea = 3f;
    public LayerMask targetMask;

    private Collider2D targetCollider2D;

    public override bool Decide(StateController_lsy controller)
    {
        return CheckTarget(controller);
    }

    private bool CheckTarget(StateController_lsy controller)
    {
        targetCollider2D = Physics2D.OverlapCircle(controller.transform.position, detectArea, targetMask);
        if (targetCollider2D != null)
        {
            controller.Target = targetCollider2D.transform;
            return true;
        }

        return false;
    }
}
