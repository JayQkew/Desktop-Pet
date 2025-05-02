using System;
using UnityEngine;

[Serializable]
public class PetPetState : PetBaseState
{
    [SerializeField] private float minPetTime;
    [SerializeField] private float currTime;
    public override void EnterState(PetStateManager manager) {
        currTime = 0;
    }

    public override void UpdateState(PetStateManager manager) {
        currTime += Time.deltaTime;
    }

    public override void ExitState(PetStateManager manager) {
    }

    public void OnDrop(PetStateManager manager) => manager.CmdSwitchState(currTime >= minPetTime ? PetState.Sleep : PetState.Idle);
}