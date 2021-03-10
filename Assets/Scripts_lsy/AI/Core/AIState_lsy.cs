using System.Collections;
using System.Collections.Generic;
//using TreeEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/State")]
public class AIState_lsy : ScriptableObject
{
    public AIAction_lsy[] Actions;
    public AITransition_lsy[] Transitions;

    public void EvaluateState(StateController_lsy controller)
    {
        DoActions(controller);
        EvaluateTransitions(controller);
    }

    public void DoActions(StateController_lsy controller)
    {
        foreach (AIAction_lsy action in Actions)
        {
            action.Act(controller);
        }
    }

    public void EvaluateTransitions(StateController_lsy controller)
    {
        if (Transitions != null || Transitions.Length > 1)
        {
            for (int i = 0; i < Transitions.Length; i++)
            {
                bool decisionResult = Transitions[i].Decision.Decide(controller);
                if (decisionResult)
                {
                    controller.TransitionToState(Transitions[i].TrueState);
                }
                else
                {
                    controller.TransitionToState(Transitions[i].FalseState);
                }
            }

        }
    }
}
