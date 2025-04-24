using System;
using UnityEngine;

[Serializable]
public class FallPetState : PetBaseState
{
    [SerializeField] private Color color = Color.white;
    [SerializeField] private LayerMask floorMask;
    private Transform _transform;

    public override void EnterState(PetStateManager manager) {
        manager.GetComponentInChildren<SpriteRenderer>().color = color;
        _transform = manager.transform;
    }

    public override void UpdateState(PetStateManager manager) {
        //do a ground check and velocity check
        if (Grounded() && manager.GetComponent<Rigidbody2D>().linearVelocity == Vector2.zero) {
            manager.SwitchState(PetState.Idle);
        }
    }

    public override void ExitState(PetStateManager manager) {
    }

    private bool Grounded() {
        return Physics2D.Raycast(_transform.position, Vector2.down, 0.27f, floorMask);
    }
}