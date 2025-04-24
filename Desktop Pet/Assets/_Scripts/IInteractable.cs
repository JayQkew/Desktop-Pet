using UnityEngine;

public interface IInteractable
{
    void OnPickup();
    void OnDrop();
    void OnHeld(Vector2 offset);
}
