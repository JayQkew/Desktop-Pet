using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class IdlePetState : PetBaseState
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private float currTime;
    [SerializeField] private Vector2 timeConstraint;
    public override void EnterState(PetStateManager manager) {
        //generate a random time between the timeConstraint
        manager.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
        currTime = Random.Range(timeConstraint.x, timeConstraint.y);
    }

    public override void UpdateState(PetStateManager manager) {
        currTime -= Time.deltaTime;
        if (currTime <= 0) {
            //choose a random state between SLEEP and WALK
            float rand = Random.Range(0f, 1f);
            PetState nextState = rand >= 0.8f ? PetState.Sleep : PetState.Walk;
            manager.SwitchState(nextState);
        }
    }

    public override void ExitState(PetStateManager manager) {
    }
}