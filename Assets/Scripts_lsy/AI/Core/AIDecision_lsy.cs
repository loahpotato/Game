using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision_lsy : ScriptableObject
{
    public abstract bool Decide(StateController_lsy controller);
}
