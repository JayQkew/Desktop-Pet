using UnityEngine;

public class WorkPetState : PetBaseState
{
    public override void EnterState(PetStateManager manager) {
        Debug.Log("Pet started working.");
    }

    public override void UpdateState(PetStateManager manager) {
    }

    public override void ExitState(PetStateManager manager) {
        Debug.Log("Pet finished working and earned a treat!");
    }
}