using UnityEngine;

public interface IInteractable
{
    void OnLeftPickup();
    void OnLeftDrop();
    void OnLeftHeld(Vector2 offset);
    
    void OnRightPickup();
    void OnRightDrop();
    void OnRightHeld(Vector2 offset);
}
