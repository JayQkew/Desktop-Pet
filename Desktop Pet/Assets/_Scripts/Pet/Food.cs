using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Food : MonoBehaviour, IInteractable
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask petLayer;
    [SerializeField] private GameObject petTarget;

    private void Update() {
        FoodRadius();
    }

    private void FoodRadius() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, radius, petLayer);
        if (hit) {
            petTarget = hit.collider.gameObject;
            petTarget.GetComponent<Pet>().targetFood = gameObject;
            Debug.Log("Food Located");
        }
    }

    public void OnLeftPickup() {
        throw new NotImplementedException();
    }

    public void OnLeftDrop() {
        throw new NotImplementedException();
    }

    public void OnLeftHeld(Vector2 offset) {
        throw new NotImplementedException();
    }

    public void OnRightPickup() {
        throw new NotImplementedException();
    }

    public void OnRightDrop() {
        throw new NotImplementedException();
    }

    public void OnRightHeld(Vector2 offset) {
        throw new NotImplementedException();
    }
}
