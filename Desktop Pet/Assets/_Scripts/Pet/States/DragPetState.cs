using System;
using UnityEngine;

[Serializable]
public class DragPetState : PetBaseState
{
    [SerializeField] private Vector2 velClamp;
    
    private Rigidbody2D rb;
    
    private Vector2 prevPos;
    private Vector2 currVel;

    private float _velocityTick = 0.01f;
    private float _currTime = 0;
    
    public override void EnterState(PetStateManager manager) {
        rb = manager.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        prevPos = manager.transform.position;
        currVel = Vector2.zero;
    }

    public override void UpdateState(PetStateManager manager) {
        rb.linearVelocity = Vector2.zero;
        _currTime += Time.deltaTime;
        if (_currTime >= _velocityTick) {
            Vector2 currPos = manager.transform.position;
            currVel = (currPos - prevPos)/_currTime;
            prevPos = currPos;
            _currTime = 0;
        }
    }

    public override void ExitState(PetStateManager manager) {
        float clampedX = Mathf.Clamp(currVel.x, -velClamp.x, velClamp.x);
        float clampedY = Mathf.Clamp(currVel.y, -velClamp.y, velClamp.y);
        Vector2 clampedVel = new Vector2(clampedX, clampedY);
        rb.linearVelocity = clampedVel;
    }
}