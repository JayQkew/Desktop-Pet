using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PetStateManager : MonoBehaviour
{
    public PetState state;

    private PetBaseState _currState;
    
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
        _currState = idleState;
    }

    private void Update() {
        _currState.UpdateState(this);

#if !UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.Alpha1)) SwitchState(PetState.Idle);
        else if(Input.GetKeyDown(KeyCode.Alpha2)) SwitchState(PetState.Walk);
        else if(Input.GetKeyDown(KeyCode.Alpha3)) SwitchState(PetState.Sleep);
        else if(Input.GetKeyDown(KeyCode.Alpha4)) SwitchState(PetState.Drag);
        else if(Input.GetKeyDown(KeyCode.Alpha5)) SwitchState(PetState.Pet);
        else if(Input.GetKeyDown(KeyCode.Alpha6)) SwitchState(PetState.Work);
        else if(Input.GetKeyDown(KeyCode.Alpha7)) SwitchState(PetState.Eat);
        else if(Input.GetKeyDown(KeyCode.Alpha8)) SwitchState(PetState.Plant);
#endif
    }

    public void SwitchState(PetState newState) {
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