using UnityEngine;

public class DragPetState : PetBaseState
{
    public override void EnterState(PetStateManager manager) {
        Debug.Log("Pet started dragging something.");
    }

    public override void UpdateState(PetStateManager manager) {
    }

    public override void ExitState(PetStateManager manager) {
        Debug.Log("Pet stopped dragging.");
    }
}