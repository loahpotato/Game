using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction_lsy : ScriptableObject
{
    public abstract void Act(StateController_lsy controller);
}
