using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class WalkPetState : PetBaseState
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private Vector2 xConstraint;
    private SpriteRenderer sr;
    public Vector2 targetPosition;
    private float currSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float foodSpeed;
    [SerializeField] private GameObject food;
    public override void EnterState(PetStateManager manager) {
        //generate a random target position
        food = manager.GetComponent<Pet>().targetFood;

        targetPosition = food?
                new Vector2(food.transform.position.x, manager.transform.position.y) :
                new Vector2(Random.Range(xConstraint.x, xConstraint.y), manager.transform.position.y);

        // ??= checks if sr is null and if it is, assigns it a value
        sr ??= manager.GetComponentInChildren<SpriteRenderer>();
        sr.sprite = sprite;
        Debug.Log("Pet is walking!");

    }

    public override void UpdateState(PetStateManager manager) {
        if(food) targetPosition = new Vector3(food.transform.position.x, manager.transform.position.y);
        float xPos = Mathf.MoveTowards(manager.transform.position.x, targetPosition.x, food ? Time.deltaTime * foodSpeed : Time.deltaTime);
        manager.transform.position = new Vector3(xPos, manager.transform.position.y, manager.transform.position.z);

        Vector2 dir = targetPosition - (Vector2)manager.transform.position;
        
        sr.flipX = dir.x != 0 ? dir.x < 0 : sr.flipX;
        
        if (Mathf.Approximately(manager.transform.position.x, targetPosition.x)) {
            manager.SwitchState(food ? PetState.Eat : PetState.Idle);
        }
    }

    public override void ExitState(PetStateManager manager) {
    }
}