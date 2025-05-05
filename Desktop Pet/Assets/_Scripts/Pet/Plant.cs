using Mirror;
using UnityEngine;

public class Plant : NetworkBehaviour, IInteractable
{
    [Header("Plant Data")]
    public bool planted;
    [SerializeField] private PlantData plantData;
    [SyncVar][SerializeField] private float currTime;
    private SpriteRenderer sr;
    
    [Header("Interaction")]
    public GameObject petTarget;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask petLayer;
    [SerializeField] private Vector2 velClamp;
    public bool canInteract;
    private Rigidbody2D rb;
    private Vector2 prevPos;
    private Vector2 currVel;
    private readonly float _velocityTick = 0.01f;
    private float _velCurrTime = 0;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }
    
    private void Update() {
        if (planted) CmdGrowPlant();
        else SeedRadius();
    }

    private void SeedRadius() {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, radius, Vector2.zero, 0, petLayer);
        if (hit && !petTarget) {
            petTarget = hit.collider.gameObject;
            petTarget.GetComponent<Pet>().Food(gameObject);
        }
    }
    
    [Server]
    private void GrowPlant() {
        if(currTime > plantData.plantStages[3].time + 1) return;
        currTime += Time.deltaTime;
        
        if (currTime >= plantData.plantStages[3].time) sr.sprite = plantData.plantStages[3].sprite;
        else if (currTime >= plantData.plantStages[2].time) sr.sprite = plantData.plantStages[2].sprite;
        else if (currTime >= plantData.plantStages[1].time) sr.sprite = plantData.plantStages[1].sprite;
        else sr.sprite = plantData.plantStages[0].sprite;
    }
    
    [Command(requiresAuthority = false)]
    private void CmdGrowPlant() => GrowPlant();
    public void OnLeftPickup() {
        if (canInteract) {
            rb.linearVelocity = Vector2.zero;
            prevPos = transform.position;
            currVel = Vector2.zero;
        }
    }

    public void OnLeftDrop() {
        if(canInteract) {
            float clampedX = Mathf.Clamp(currVel.x, -velClamp.x, velClamp.x);
            float clampedY = Mathf.Clamp(currVel.y, -velClamp.y, velClamp.y);
            Vector2 clampedVel = new Vector2(clampedX, clampedY);
            rb.linearVelocity = clampedVel;
        }
    }

    public void OnLeftHeld(Vector2 offset) {
        if(canInteract) {
            rb.MovePosition(offset);
            rb.linearVelocity = Vector2.zero;
            _velCurrTime += Time.deltaTime;
            if (_velCurrTime >= _velocityTick) {
                Vector2 currPos = transform.position;
                currVel = (currPos - prevPos) / _velCurrTime;
                prevPos = currPos;
                _velCurrTime = 0;
            }
        }
    }

    public void OnRightPickup() {
    }

    public void OnRightDrop() {
    }

    public void OnRightHeld(Vector2 offset) {
    }

}
