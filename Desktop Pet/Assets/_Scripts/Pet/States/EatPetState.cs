using System;
using UnityEngine;

[Serializable]
public class EatPetState : PetBaseState
{
    [SerializeField] private Color color = Color.white;
    public Food food;
    public override void EnterState(PetStateManager manager) {
        manager.GetComponentInChildren<SpriteRenderer>().color = color;
    }

    public override void UpdateState(PetStateManager manager) {
        Vector2 foodPos = food.transform.position;
        Vector2 foodPrevPos = manager.walkState.targetPosition;
        if(!Mathf.Approximately(foodPos.x, foodPrevPos.x)) manager.SwitchState(PetState.Walk);
    }

    public override void ExitState(PetStateManager manager) {
        Debug.Log("Pet is full!");
    }
}