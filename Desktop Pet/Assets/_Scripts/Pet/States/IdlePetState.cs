using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class IdlePetState : PetBaseState
{
    [SerializeField] private Color color = Color.white;
    [SerializeField] private float currTime;
    [SerializeField] private Vector2 timeConstraint;
    public override void EnterState(PetStateManager manager) {
        Debug.Log("Pet is now idle.");
        //generate a random time between the timeConstraint
        currTime = Random.Range(timeConstraint.x, timeConstraint.y);
        manager.GetComponentInChildren<SpriteRenderer>().color = color;
    }

    public override void UpdateState(PetStateManager manager) {
        currTime -= Time.deltaTime;
        if (currTime <= 0) {
            //choose a random state between SLEEP and WALK
            manager.SwitchState(PetState.Sleep);
        }
    }

    public override void ExitState(PetStateManager manager) {
        Debug.Log("Pet is leaving the idle state.");        
    }
}