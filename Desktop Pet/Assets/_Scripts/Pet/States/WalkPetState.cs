using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class WalkPetState : PetBaseState
{
    [SerializeField] private Color color = Color.white;
    [SerializeField] private Vector2 xConstraint;
    [SerializeField] private Vector2 targetPosition;
    [SerializeField] private float speed;
    public override void EnterState(PetStateManager manager) {
        Debug.Log("Pet started walking.");
        //generate a random target position
        targetPosition = new Vector2(Random.Range(xConstraint.x, xConstraint.y), manager.transform.position.y);
        manager.GetComponentInChildren<SpriteRenderer>().color = color;
    }

    public override void UpdateState(PetStateManager manager) {
        float xPos = Mathf.MoveTowards(manager.transform.position.x, targetPosition.x, speed * Time.deltaTime);
        manager.transform.position = new Vector3(xPos, manager.transform.position.y, manager.transform.position.z);

        if (manager.transform.position.x == targetPosition.x) {
            manager.SwitchState(PetState.Idle);
        }
    }

    public override void ExitState(PetStateManager manager) {
        Debug.Log("Pet stopped walking.");
    }
}