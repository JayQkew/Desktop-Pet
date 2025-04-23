using System;
using UnityEngine;

public class PetStateManager : MonoBehaviour
{
    public PetState state;

    public PetBaseState currState;

    private void Start() {
        
    }

    private void Update() {
        currState.UpdateState(this);
    }

    private void SwitchState(PetState newState) {
        state = newState;
        switch (newState) {
            case PetState.Idle:
                break;
            case PetState.Walk:
                break;
            case PetState.Sleep:
                break;
            case PetState.Drag:
                break;
            case PetState.Pet:
                break;
            case PetState.Work:
                break;
            case PetState.Eat:
                break;
            case PetState.Plant:
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
}

public class IdlePetState : PetBaseState
{
    public override void EnterState(PetStateManager manager) {
        
    }

    public override void UpdateState(PetStateManager manager) {
        
    }
}
