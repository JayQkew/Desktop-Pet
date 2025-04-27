using System;
using UnityEngine;
using UnityEngine.Serialization;

[SelectionBase]
public class Food : MonoBehaviour, IInteractable
{
    public GameObject petTarget;
    public float foodAmount; // time taken to eat food
    [SerializeField] private float radius;
    [SerializeField] private LayerMask petLayer;
    [SerializeField] private Vector2 velClamp;

    private Rigidbody2D rb;
    private Vector2 prevPos;
    private Vector2 currVel;

    private float _velocityTick = 0.01f;
    private float _currTime = 0;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        FoodRadius();
    }

    private void FoodRadius() {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, radius, Vector2.zero, 0, petLayer);
        if (hit && !petTarget) {
            petTarget = hit.collider.gameObject;
            petTarget.GetComponent<Pet>().Food(gameObject);
        }
    }

    public void OnLeftPickup() {
        rb.linearVelocity = Vector2.zero;
        prevPos = transform.position;
        currVel = Vector2.zero;
    }

    public void OnLeftDrop() {
        float clampedX = Mathf.Clamp(currVel.x, -velClamp.x, velClamp.x);
        float clampedY = Mathf.Clamp(currVel.y, -velClamp.y, velClamp.y);
        Vector2 clampedVel = new Vector2(clampedX, clampedY);
        rb.linearVelocity = clampedVel;
    }

    public void OnLeftHeld(Vector2 offset) {
        rb.MovePosition(offset);
        rb.linearVelocity = Vector2.zero;
        _currTime += Time.deltaTime;
        if (_currTime >= _velocityTick) {
            Vector2 currPos = transform.position;
            currVel = (currPos - prevPos)/_currTime;
            prevPos = currPos;
            _currTime = 0;
        }
    }

    public void OnRightPickup() {
    }

    public void OnRightDrop() {
    }

    public void OnRightHeld(Vector2 offset) {
    }
}
