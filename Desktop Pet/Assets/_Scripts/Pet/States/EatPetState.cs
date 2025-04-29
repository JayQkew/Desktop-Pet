using System;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class EatPetState : PetBaseState
{
    [SerializeField] private Color color = Color.white;
    public Food food;
    public override void EnterState(PetStateManager manager) {
        manager.GetComponentInChildren<SpriteRenderer>().color = color;
        food = manager.GetComponent<Pet>().targetFood.GetComponent<Food>();
        manager.walkState.targetPosition = food.transform.position;
    }

    public override void UpdateState(PetStateManager manager) {
        food.foodAmount -= Time.deltaTime;
        manager.GetComponent<Pet>().foodEaten += Time.deltaTime;
        
        if(food.foodAmount <= 0) {
            Object.Destroy(food.gameObject);
            manager.SwitchState(PetState.Idle);
        }
        
        Vector2 foodPos = food.transform.position;
        Vector2 foodPrevPos = manager.walkState.targetPosition;
        if(Vector2.Distance(foodPos, foodPrevPos) > 0.1f) manager.SwitchState(PetState.Walk);
    }

    public override void ExitState(PetStateManager manager) {
        Debug.Log("Pet is full!");
    }
}