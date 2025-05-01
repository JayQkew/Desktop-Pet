using System;
using UnityEngine;

[Serializable]
public class PetPetState : PetBaseState
{
    public override void EnterState(PetStateManager manager) {
        Debug.Log("Pet is being petted. Purrrr...");
    }

    public override void UpdateState(PetStateManager manager) {
    }

    public override void ExitState(PetStateManager manager) {
        Debug.Log("Petting session ended.");
    }
}