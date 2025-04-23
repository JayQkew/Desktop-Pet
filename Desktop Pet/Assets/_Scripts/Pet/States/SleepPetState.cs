using System;
using UnityEngine;

[Serializable]
public class SleepPetState : PetBaseState
{
    public override void EnterState(PetStateManager manager) {
        Debug.Log("Pet started sleeping. Zzz...");
    }

    public override void UpdateState(PetStateManager manager) {
    }

    public override void ExitState(PetStateManager manager) {
        Debug.Log("Pet woke up, feeling refreshed!");
    }
}