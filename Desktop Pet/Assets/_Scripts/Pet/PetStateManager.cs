using System;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;

public class PetStateManager : NetworkBehaviour
{
    public PetState state;

    public PetBaseState currState;
    
    public IdlePetState idleState = new IdlePetState();
    public WalkPetState walkState = new WalkPetState();
    public SleepPetState sleepState = new SleepPetState();
    public DragPetState dragState = new DragPetState();
    public PetPetState petState = new PetPetState();
    public WorkPetState workState = new WorkPetState();
    public EatPetState eatState = new EatPetState();
    public PlantPetState plantState = new PlantPetState();
    public FallPetState fallState = new FallPetState();

    private void Start() {
        currState = idleState;
    }

    private void Update() {
        currState.UpdateState(this);
    }

    [Command]
    public void SwitchState(PetState petState) {
        ServerSwitchState(petState);
    }
    
    [ClientRpc]
    public void ServerSwitchState(PetState newState) {
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