using System;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : NetworkBehaviour
{
    [SyncVar]
    public string playerName;

    [Space(10)]
    private GameObject _pet;
    public Textbox textbox;
    private InputHandler _inputHandler;
    // [SerializeField] private GameObject[] hoverObjects = Array.Empty<GameObject>();
    [SerializeField] private GameObject heldObject;
    private IInteractable _interactable;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private LayerMask hoverLayer;
    private Vector2 offset;

    private void Awake() {
        _pet = transform.GetChild(0).gameObject;
        _inputHandler = GetComponent<InputHandler>();
        textbox = GetComponent<Textbox>();
    }

    private void Start() {
        _inputHandler.onLeftDown.AddListener(LeftDown);
        _inputHandler.onLeftHold.AddListener(LeftHold);
        _inputHandler.onLeftUp.AddListener(LeftUp);
        
        _inputHandler.onRightDown.AddListener(RightDown);
        _inputHandler.onRightHold.AddListener(RightHold);
        _inputHandler.onRightUp.AddListener(RightUp);
    }

    private void Update() {
        if(isServer) ServerHover();
        else if(isLocalPlayer) Hover();
    }

    public override void OnStartClient() {
        base.OnStartClient();
        if(isServer) textbox.ServerDisplayYou();
        else if(isLocalPlayer) textbox.DisplayYou();
        else textbox.DisplayText("");
    }
    
    [TargetRpc]
    private void Hover() {
        Collider2D hit = Physics2D.OverlapPoint(_inputHandler.mousePos, hoverLayer);

        if (hit == null) {
            if(isServer) textbox.ServerDisplayYou();
            else if(isLocalPlayer) textbox.DisplayYou();
            return;
        }
        
        GameObject hitObject = hit.gameObject;
        if (hitObject == _pet) {
            if(isServer) textbox.ServerOwnPet(playerName);
            else if (isLocalPlayer) textbox.DisplayOwnPet(playerName);
        }
        else {
            if(isServer) textbox.ServerOtherPet(playerName);
            else if (isLocalPlayer) textbox.DisplayOtherPet(playerName);
        }
    }

    [Server]
    private void ServerHover() => Hover();

    private void LeftDown() {
        Collider2D hit = Physics2D.OverlapPoint(_inputHandler.mousePos, interactableLayer);
        if (hit != null) {
            heldObject = hit.gameObject;
            _interactable = hit.GetComponent<IInteractable>();
            offset = (Vector2)heldObject.transform.position - _inputHandler.mousePos;
            
            _interactable.OnLeftPickup();
        }
    }

    private void LeftHold() {
        if (_interactable != null) {
            _interactable.OnLeftHeld(offset + _inputHandler.mousePos);
        }
    }

    private void LeftUp() {
        if (_interactable != null) {
            _interactable.OnLeftDrop();

            heldObject = null;
            _interactable = null;
        }
    }

    private void RightDown() {
        Collider2D hit = Physics2D.OverlapPoint(_inputHandler.mousePos, interactableLayer);
        if (hit != null) {
            heldObject = hit.gameObject;
            _interactable = hit.GetComponent<IInteractable>();

            _interactable.OnRightPickup();
        }
    }

    private void RightHold() {
        
    }

    private void RightUp() {
        if (_interactable != null) {
            _interactable.OnRightDrop();
            heldObject = null;
            _interactable = null;
        }
    }
}
