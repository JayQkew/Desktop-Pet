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