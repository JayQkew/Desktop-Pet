using System;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;

public class PetStateManager : NetworkBehaviour
{
    public PetState state;

    private PetBaseState _currState;
    
    public IdlePetState idleState = new IdlePetState();
    public WalkPetState walkState = new WalkPetState();
    public SleepPetState sleepState = new SleepPetState();
    public DragPetState dragState = new DragPetState();
    public PetPetState petState = new PetPetState();
    public WorkPetState workState = new WorkPetState();
    public EatPetState eatState = new EatPetState();
    public PlantPetState plantState = new PlantPetState();
    public FallPetState fallState = new FallPetState();

    private PetGUI _gui;

    private void Awake() {
        _gui = GetComponentInChildren<PetGUI>();
    }

    private void Start() {
        _currState = idleState;
    }

    private void Update() {
        _currState.UpdateState(this);
    }

    [Command]
    public void CmdSwitchState(PetState petState) {
        RpcSwitchState(petState);
    }
    
    [ClientRpc]
    private void RpcSwitchState(PetState newState) {
        state = newState;
        _currState.ExitState(this);
        switch (newState) {
            case PetState.Idle:
                _currState = idleState;
                break;
            case PetState.Walk:
                _currState = walkState;
                break;
            case PetState.Sleep:
                _currState = sleepState;
                break;
            case PetState.Drag:
                _currState = dragState;
                break;
            case PetState.Pet:
                _currState = petState;
                break;
            case PetState.Work:
                _currState = workState;
                break;
            case PetState.Eat:
                _currState = eatState;
                break;
            case PetState.Plant:
                _currState = plantState;
                break;
            case PetState.Fall:
                _currState = fallState;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        _currState.EnterState(this);
        _gui.SetAnim(newState);
    }
}

public enum PetState
{
    Idle = 0,
    Walk = 1,
    Sleep = 2,
    Drag = 3,
    Pet = 4,
    Work = 5,
    Eat = 6,
    Plant = 7,
    Fall = 8
}

public abstract class PetBaseState
{
    public abstract void EnterState(PetStateManager manager);
    public abstract void UpdateState(PetStateManager manager);
    public abstract void ExitState(PetStateManager manager);
}