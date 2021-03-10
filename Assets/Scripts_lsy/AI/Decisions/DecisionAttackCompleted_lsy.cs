using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Attack Completed", fileName = "DecisionAttackCompleted")]
public class DecisionAttackCompleted_lsy : AIDecision_lsy
{
    public override bool Decide(StateController_lsy controller)
    {
        return AttackCompleted(controller);
    }

    private bool AttackCompleted(StateController_lsy controller)
    {
        if (controller.CharacterWeapon.CurrentWeapon.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).length
        > controller.CharacterWeapon.CurrentWeapon.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime)
        {
            return true;
        }

        return false;
    }
}
