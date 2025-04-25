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

    private void Update() {
        if (petStateManager.state == PetState.Drag) {
            
        }
    }

    public void OnPickup() {
        petStateManager.SwitchState(PetState.Drag);
        Debug.Log("Pet picked up");
    }

    public void OnDrop() {
        petStateManager.SwitchState(PetState.Fall);
        Debug.Log("Pet dropped");
    }

    public void OnHeld(Vector2 offset) {
        // transform.position = offset;
        rb.MovePosition(offset);
        Debug.Log("Pet held");
    }
}
