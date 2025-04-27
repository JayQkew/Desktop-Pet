using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PetStateManager : MonoBehaviour
{
    public PetState state;

    public PetBaseState currState;
    
    [SerializeField] IdlePetState idleState = new IdlePetState();
    [SerializeField] WalkPetState walkState = new WalkPetState();
    [SerializeField] SleepPetState sleepState = new SleepPetState();
    [SerializeField] DragPetState dragState = new DragPetState();
    [SerializeField] PetPetState petState = new PetPetState();
    [SerializeField] WorkPetState workState = new WorkPetState();
    [SerializeField] EatPetState eatState = new EatPetState();
    [SerializeField] PlantPetState plantState = new PlantPetState();
    [SerializeField] FallPetState fallState = new FallPetState();

    private void Start() {
        currState = idleState;
    }

    private void Update() {
        currState.UpdateState(this);
    }

    public void SwitchState(PetState newState) {
        state = newState;
        currState.ExitState(this);
        switch (newState) {
            case PetState.Idle:
                currState = idleState;
                break;
            case PetState.Walk:
                currState = walkState;
                break;
            case PetState.Sleep:
                currState = sleepState;
                break;
            case PetState.Drag:
                currState = dragState;
                break;
            case PetState.Pet:
                currState = petState;
                break;
            case PetState.Work:
                currState = workState;
                break;
            case PetState.Eat:
                currState = eatState;
                break;
            case PetState.Plant:
                currState = plantState;
                break;
            case PetState.Fall:
                currState = fallState;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        currState.EnterState(this);
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
    Plant,
    Fall
}

public abstract class PetBaseState
{
    public abstract void EnterState(PetStateManager manager);
    public abstract void UpdateState(PetStateManager manager);
    public abstract void ExitState(PetStateManager manager);
}