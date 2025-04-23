using UnityEngine;

public class WalkPetState : PetBaseState
{
    public override void EnterState(PetStateManager manager) {
        Debug.Log("Pet started walking.");
    }

    public override void UpdateState(PetStateManager manager) {
    }

    public override void ExitState(PetStateManager manager) {
        Debug.Log("Pet stopped walking.");
    }
}