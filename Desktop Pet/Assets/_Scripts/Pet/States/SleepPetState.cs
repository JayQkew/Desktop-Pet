using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class SleepPetState : PetBaseState
{
    [SerializeField] private float currTime;
    [SerializeField] private Vector2 timeConstraint;
    public override void EnterState(PetStateManager manager) {
        currTime = Random.Range(timeConstraint.x, timeConstraint.y);
    }

    public override void UpdateState(PetStateManager manager) {
        currTime -= Time.deltaTime;
        if (currTime <= 0) {
            //choose a random state between SLEEP and WALK
            float rand = Random.Range(0f, 1f);
            PetState nextState = rand >= 0.5f ? PetState.Walk : PetState.Idle;
            manager.CmdSwitchState(nextState);
        }
    }

    public override void ExitState(PetStateManager manager) {
    }
}