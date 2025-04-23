using System;
using UnityEngine;

[Serializable]
public class EatPetState : PetBaseState
{
    public override void EnterState(PetStateManager manager) {
        Debug.Log("Pet started eating. Nom nom nom...");
    }

    public override void UpdateState(PetStateManager manager) {
    }

    public override void ExitState(PetStateManager manager) {
        Debug.Log("Pet is full!");
    }
}