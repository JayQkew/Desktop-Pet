using System;
using UnityEngine;

[Serializable]
public class DragPetState : PetBaseState
{
    [SerializeField] private Color color = Color.white;
    public override void EnterState(PetStateManager manager) {
        manager.GetComponentInChildren<SpriteRenderer>().color = color;
    }

    public override void UpdateState(PetStateManager manager) {
        manager.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }

    public override void ExitState(PetStateManager manager) {
    }
}