using System;
using UnityEngine;

[Serializable]
public class FallPetState : PetBaseState
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private LayerMask floorMask;
    [SerializeField] private float decelerationSpeed = 1f;
    [SerializeField] private float fallSpeed = 1f;
    private Transform _transform;
    private Rigidbody2D rb;

    public override void EnterState(PetStateManager manager) {
        if(!rb) rb = manager.GetComponent<Rigidbody2D>();
        manager.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
        _transform = manager.transform;
    }

    public override void UpdateState(PetStateManager manager) {
        //do a ground check and velocity check
        if (Grounded() && rb.linearVelocity == Vector2.zero) {
            manager.CmdSwitchState(PetState.Idle);
        }

        if (rb.linearVelocityY < 0) {
            //clamp the y velocity
            //lerp the x velocity to 0
            rb.linearVelocityY = Mathf.Lerp(rb.linearVelocityY, fallSpeed, decelerationSpeed * Time.deltaTime);
            rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, 0, Time.deltaTime * decelerationSpeed);
        }
    }

    public override void ExitState(PetStateManager manager) {
    }

    private bool Grounded() {
        return Physics2D.Raycast(_transform.position, Vector2.down, 0.51f, floorMask);
    }
}