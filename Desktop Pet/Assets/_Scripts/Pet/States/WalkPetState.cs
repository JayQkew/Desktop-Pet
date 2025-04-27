using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class WalkPetState : PetBaseState
{
    [SerializeField] private Color color = Color.white;
    [SerializeField] private Vector2 xConstraint;
    public Vector2 targetPosition;
    [SerializeField] private float speed;
    [SerializeField] private GameObject food;
    public override void EnterState(PetStateManager manager) {
        //generate a random target position
        food = manager.GetComponent<Pet>().targetFood;

        targetPosition = food?
                new Vector2(food.transform.position.x, manager.transform.position.y) :
                new Vector2(Random.Range(xConstraint.x, xConstraint.y), manager.transform.position.y);
        manager.GetComponentInChildren<SpriteRenderer>().color = color;
        Debug.Log("Pet is walking!");

    }

    public override void UpdateState(PetStateManager manager) {
        if(food) targetPosition = new Vector3(food.transform.position.x, manager.transform.position.y);
        float xPos = Mathf.MoveTowards(manager.transform.position.x, targetPosition.x, Time.deltaTime);
        manager.transform.position = new Vector3(xPos, manager.transform.position.y, manager.transform.position.z);

        if (Mathf.Approximately(manager.transform.position.x, targetPosition.x)) {
            manager.SwitchState(food ? PetState.Eat : PetState.Idle);
        }
    }

    public override void ExitState(PetStateManager manager) {
    }
}