using System;
using UnityEngine;

public class Pet : MonoBehaviour, IInteractable
{
    private PetStateManager petStateManager;
    private Rigidbody2D rb;

    [Header("Poop")]
    [SerializeField] private GameObject poopPrefab;
    [SerializeField] private float poopForce;
    [SerializeField] private float poopTimer;
    [SerializeField] private float poopCurrTime;

    [Header("Food")]
    public GameObject targetFood;

    private void Awake() {
        petStateManager = GetComponent<PetStateManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        poopCurrTime -= Time.deltaTime;
        if (poopCurrTime <= 0) {
            Poop();
            poopCurrTime = poopTimer;
        }
    }

    private void Poop() {
        GameObject poop = Instantiate(poopPrefab, transform.position, Quaternion.identity);
        Rigidbody2D poopRb = poop.GetComponent<Rigidbody2D>();
        poopRb.AddForce(Vector2.up * poopForce, ForceMode2D.Impulse);
    }

    public void Food(GameObject food) {
        targetFood = food;
        petStateManager.SwitchState(PetState.Walk);
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
