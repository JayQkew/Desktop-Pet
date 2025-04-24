using UnityEngine;

public class Pet : MonoBehaviour, IInteractable
{
    public void OnPickup() {
        Debug.Log("Pet picked up");
    }

    public void OnDrop() {
        Debug.Log("Pet dropped");
    }

    public void OnHeld() {
        Debug.Log("Pet held");
    }
}
