using System;
using UnityEngine;

[Serializable]
public class IdlePetState : PetBaseState
{
    public override void EnterState(PetStateManager manager) {
        Debug.Log("Pet is now idle.");
    }

    public override void UpdateState(PetStateManager manager) {
    }

    public override void ExitState(PetStateManager manager) {
        Debug.Log("Pet is leaving the idle state.");
    }
}