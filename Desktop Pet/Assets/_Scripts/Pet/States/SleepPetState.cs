using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class SleepPetState : PetBaseState
{
    [SerializeField] private float currTime;
    [SerializeField] private Vector2 timeConstraint;
    public override void EnterState(PetStateManager manager) {
        Debug.Log("Pet started sleeping. Zzz...");
        currTime = Random.Range(timeConstraint.x, timeConstraint.y);
    }

    public override void UpdateState(PetStateManager manager) {
        currTime -= Time.deltaTime;
        if (currTime <= 0) {
            //choose a random state between SLEEP and WALK
            manager.SwitchState(PetState.Walk);
        }
    }

    public override void ExitState(PetStateManager manager) {
        Debug.Log("Pet woke up, feeling refreshed!");        
    }
}