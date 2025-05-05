using System;
using Mirror;
using UnityEngine;

[SelectionBase]
public class Pet : NetworkBehaviour, IInteractable
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
        if(!isServer) return;
        
        if (foodEaten >= poopThreshold) {
            poopCurrTime -= Time.deltaTime;
            if (poopCurrTime <= 0) {
                CmdSpawnPoop();
                poopCurrTime = poopTimer;
                foodEaten -= poopThreshold;
            }
        }
    }
    
    [Server]
    private void SpawnPoop() {
        GameObject poop =  Instantiate(poopPrefab, transform.position, Quaternion.identity);
        NetworkServer.Spawn(poop);
        Rigidbody2D poopRb = poop.GetComponent<Rigidbody2D>();
        poopRb.AddForce(Vector2.up * poopForce, ForceMode2D.Impulse);
    }

    [Command(requiresAuthority = false)]
    private void CmdSpawnPoop() => SpawnPoop();

    public void Food(GameObject food) {
        targetFood = food;
        _petStateManager.CmdSwitchState(PetState.Walk);
    }
    
    [Command]
    public void CmdDestroyItem() => RpcDestroyItem();

    [ClientRpc]
    private void RpcDestroyItem() => Destroy(targetFood);

    [Command]
    public void CmdFlipSprite(bool flip) => RpcFlipSprite(flip);

    [ClientRpc]
    private void RpcFlipSprite(bool flip) {
        _guiTransform.localScale = !flip ? new Vector3(1f, 1f, 1f) : new Vector3(-1f, 1f, 1f);
    }
    public void OnLeftPickup() {
        _petStateManager.CmdSwitchState(PetState.Drag);
    }

    public void OnLeftDrop() {
        _petStateManager.CmdSwitchState(PetState.Fall);
    }

    public void OnLeftHeld(Vector2 offset) {
        // transform.position = offset;
        _rb.MovePosition(offset);
    }

    public void OnRightPickup() {
        _petStateManager.CmdSwitchState(PetState.Pet);
    }

    public void OnRightDrop() {
        // after a long petting session, the frog falls asleep
        _petStateManager.petState.OnDrop(_petStateManager);
    }

    public void OnRightHeld(Vector2 offset) {
    }
}
