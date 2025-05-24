using System;
using Mirror;
using UnityEngine;

[SelectionBase]
public class Pet : MonoBehaviour, IInteractable
{
    private PetStateManager _petStateManager;
    private Rigidbody2D _rb;
    private Transform _guiTransform;

    [Header("Poop")]
    [SerializeField] private GameObject poopPrefab;
    [SerializeField] private float poopForce;
    [SerializeField] private float poopTimer;
    [SerializeField] private float poopCurrTime;

    [Header("Food")]
    public GameObject targetFood;
    public float foodEaten;
    public float poopThreshold;

    private void Awake() {
        _petStateManager = GetComponent<PetStateManager>();
        _rb = GetComponent<Rigidbody2D>();
        _guiTransform = transform.GetChild(0);
        poopCurrTime = poopTimer;
    }

    private void Update() {
        if (foodEaten >= poopThreshold) {
            poopCurrTime -= Time.deltaTime;
            if (poopCurrTime <= 0) {
                SpawnPoop();
                poopCurrTime = poopTimer;
                foodEaten -= poopThreshold;
            }
        }
    }
    
    private void SpawnPoop() {
        GameObject poop =  Instantiate(poopPrefab, transform.position, Quaternion.identity);
        NetworkServer.Spawn(poop);
        Rigidbody2D poopRb = poop.GetComponent<Rigidbody2D>();
        poopRb.AddForce(Vector2.up * poopForce, ForceMode2D.Impulse);
    }

    public void Food(GameObject food) {
        targetFood = food;
        _petStateManager.SwitchState(PetState.Walk);
    }

    public void DestroyItem() => Destroy(targetFood);

    public void FlipSprite(bool flip) {
        _guiTransform.localScale = !flip ? new Vector3(1f, 1f, 1f) : new Vector3(-1f, 1f, 1f);
    }
    public void OnLeftPickup() {
        _petStateManager.SwitchState(PetState.Drag);
    }

    public void OnLeftDrop() {
        _petStateManager.SwitchState(PetState.Fall);
    }

    public void OnLeftHeld(Vector2 offset) {
        // transform.position = offset;
        _rb.MovePosition(offset);
    }

    public void OnRightPickup() {
        _petStateManager.SwitchState(PetState.Pet);
    }

    public void OnRightDrop() {
        // after a long petting session, the frog falls asleep
        _petStateManager.petState.OnDrop(_petStateManager);
    }

    public void OnRightHeld(Vector2 offset) {
    }
}
