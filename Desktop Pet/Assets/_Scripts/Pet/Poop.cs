using UnityEngine;

[SelectionBase]
public class Poop : MonoBehaviour, IInteractable
{
    public void OnLeftPickup() {
        Destroy(gameObject);
    }

    public void OnLeftDrop() {
    }

    public void OnLeftHeld(Vector2 offset) {
    }

    public void OnRightPickup() {
    }

    public void OnRightDrop() {
    }

    public void OnRightHeld(Vector2 offset) {
    }
}
