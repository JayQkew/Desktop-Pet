using System;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class EatPetState : PetBaseState
{
    public Food food;
    private Pet pet;
    public override void EnterState(PetStateManager manager) {
        pet ??= manager.GetComponent<Pet>();
        food = pet.targetFood.GetComponent<Food>();
        manager.walkState.targetPosition = food.transform.position;
    }

    public override void UpdateState(PetStateManager manager) {
        food.foodAmount -= Time.deltaTime;
        pet.foodEaten += Time.deltaTime;
        
        if(food.foodAmount <= 0) {
            pet.CmdEatFood();
            manager.CmdSwitchState(PetState.Idle);
        }
        
        Vector2 foodPos = food.transform.position;
        Vector2 foodPrevPos = manager.walkState.targetPosition;
        if(Vector2.Distance(foodPos, foodPrevPos) > 0.1f) manager.CmdSwitchState(PetState.Walk);
    }

    public override void ExitState(PetStateManager manager) {
    }
}