using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[Serializable]
public class WalkPetState : PetBaseState
{
    [SerializeField] private Vector2 xConstraint;
    private SpriteRenderer sr;
    private Pet pet;
    public Vector2 targetPosition;
    private float currSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float targetItemSpeed;
    [SerializeField] private GameObject targetItem;

    public override void EnterState(PetStateManager manager) {
        //generate a random target position
        targetItem = manager.GetComponent<Pet>().targetFood;

        targetPosition = targetItem
            ? new Vector2(targetItem.transform.position.x, manager.transform.position.y)
            : new Vector2(Random.Range(xConstraint.x, xConstraint.y), manager.transform.position.y);

        // ??= checks if sr is null and if it is, assigns it a value
        sr ??= manager.GetComponentInChildren<SpriteRenderer>();

        pet ??= manager.GetComponent<Pet>();
    }

    public override void UpdateState(PetStateManager manager) {
        if (targetItem) targetPosition = new Vector3(targetItem.transform.position.x, manager.transform.position.y);
        float xPos = Mathf.MoveTowards(manager.transform.position.x, targetPosition.x,
            targetItem ? Time.deltaTime * targetItemSpeed : Time.deltaTime);
        manager.transform.position = new Vector3(xPos, manager.transform.position.y, manager.transform.position.z);

        Vector2 dir = targetPosition - (Vector2)manager.transform.position;

        pet.CmdFlipSprite(dir.x != 0 ? dir.x < 0 : sr.flipX);

        if (Mathf.Approximately(manager.transform.position.x, targetPosition.x)) {
            PetState newState = PetState.Idle;

            if (targetItem) {
                if (targetItem.GetComponent<Food>() != null) {
                    newState = PetState.Eat;
                }
                else if (targetItem.GetComponent<Plant>() != null) {
                    newState = PetState.Plant;
                }
            }
            
            manager.CmdSwitchState(newState);
        }
    }

    public override void ExitState(PetStateManager manager) {
    }
}