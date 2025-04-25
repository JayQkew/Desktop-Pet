using System;
using UnityEngine;

public class Pet : MonoBehaviour, IInteractable
{
    private PetStateManager petStateManager;
    private Rigidbody2D rb;

    private void Awake() {
        petStateManager = GetComponent<PetStateManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnLeftPickup() {
        petStateManager.SwitchState(PetState.Drag);
        Debug.Log("Pet picked up");
    }

    public void OnLeftDrop() {
        petStateManager.SwitchState(PetState.Fall);
        Debug.Log("Pet dropped");
    }

    public void OnLeftHeld(Vector2 offset) {
        // transform.position = offset;
        rb.MovePosition(offset);
        Debug.Log("Pet held");
    }

    public void OnRightPickup() {
        petStateManager.SwitchState(PetState.Pet);
    }

    public void OnRightDrop() {
        // after a long petting session, the frog falls asleep
        petStateManager.SwitchState(PetState.Sleep);
    }

    public void OnRightHeld(Vector2 offset) {
    }
}
