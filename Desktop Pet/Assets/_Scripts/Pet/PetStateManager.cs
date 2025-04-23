using System;
using UnityEngine;

public class PetStateManager : MonoBehaviour
{
    public PetState state;

    public PetBaseState currState;
    
    public IdlePetState IdleState = new IdlePetState();
    public WalkPetState WalkState = new WalkPetState();
    public SleepPetState SleepState = new SleepPetState();
    public DragPetState DragState = new DragPetState();
    public PetPetState PetState = new PetPetState();
    public WorkPetState WorkState = new WorkPetState();
    public EatPetState EatState = new EatPetState();
    public PlantPetState PlantState = new PlantPetState();

    private void Start() {
        
    }

    private void Update() {
        currState.UpdateState(this);
    }

    private void SwitchState(PetState newState) {
        state = newState;
        switch (newState) {
            case global::PetState.Idle:
                break;
            case global::PetState.Walk:
                break;
            case global::PetState.Sleep:
                break;
            case global::PetState.Drag:
                break;
            case global::PetState.Pet:
                break;
            case global::PetState.Work:
                break;
            case global::PetState.Eat:
                break;
            case global::PetState.Plant:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}

public enum PetState
{
    Idle,
    Walk,
    Sleep,
    Drag,
    Pet,
    Work,
    Eat,
    Plant
}

public abstract class PetBaseState
{
    public abstract void EnterState(PetStateManager manager);
    public abstract void UpdateState(PetStateManager manager);
    public abstract void ExitState(PetStateManager manager);
}

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

public class PlantPetState : PetBaseState
{
    public override void EnterState(PetStateManager manager) {
        Debug.Log("Pet started planting something.");
    }

    public override void UpdateState(PetStateManager manager) {
    }

    public override void ExitState(PetStateManager manager) {
        Debug.Log("Pet finished planting.");
    }
}


