using System;
using UnityEngine;

[Serializable]
public class DragPetState : PetBaseState
{
    [SerializeField] private Color color = Color.white;

    private Rigidbody2D rb;
    
    private Vector2 prevPos;
    private Vector2 currVel;

    private float _velocityTick = 0.05f;
    private float _currTime = 0;
    
    public override void EnterState(PetStateManager manager) {
        manager.GetComponentInChildren<SpriteRenderer>().color = color;
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
        rb.linearVelocity = currVel;
    }
}