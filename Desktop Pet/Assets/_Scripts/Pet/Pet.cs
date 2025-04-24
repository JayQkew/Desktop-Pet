using System;
using UnityEngine;

public class Pet : MonoBehaviour, IInteractable
{
    PetStateManager petStateManager;

    private void Awake() {
        petStateManager = GetComponent<PetStateManager>();
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
        transform.position = offset;
        Debug.Log("Pet held");
    }
}
