using Mirror;
using UnityEngine;

[SelectionBase]
public class Poop : MonoBehaviour, IInteractable
{
    private void DestroyPoop() {
        Destroy(gameObject);
    }
    
    public void OnLeftPickup() => DestroyPoop();

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
