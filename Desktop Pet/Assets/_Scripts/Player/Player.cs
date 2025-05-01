using System;
using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SyncVar]
    public string playerName;

    [Space(10)]
    private GameObject _pet;
    private Textbox _textbox;
    private InputHandler _inputHandler;
    [SerializeField] private GameObject[] hoverObjects = Array.Empty<GameObject>();
    [SerializeField] private GameObject heldObject;
    private IInteractable _interactable;
    [SerializeField] private LayerMask interactableLayer;
    private Vector2 offset;

    private void Awake() {
        _pet = transform.GetChild(0).gameObject;
        _inputHandler = GetComponent<InputHandler>();
        _textbox = GetComponent<Textbox>();
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
        Hover();
    }

    public override void OnStartClient() {
        base.OnStartClient();
        if(isLocalPlayer) CmdDisplayYou();
        else _textbox.DisplayText("");
    }

    [Command]
    private void CmdDisplayYou() {
        _textbox.ServerDisplayYou();
    }
    
    private void Hover() {
        //on the client, this is always empty
        Collider2D[] hit = Physics2D.OverlapPointAll(_inputHandler.mousePos, interactableLayer);

        if (hit.Length == 0) {
            Debug.Log(gameObject.name + " is not Hovering");
            CmdDisplayYou();
            hoverObjects = Array.Empty<GameObject>();
            return;
        }
        
        GameObject[] hitGameObjects = new GameObject[hit.Length];
        for (int i = 0; i < hit.Length; i++) {
            hitGameObjects[i] = hit[i].gameObject;
            Player hitPlayer = hitGameObjects[i].GetComponentInParent<Player>();

            if (hitPlayer == null) continue;
            
            if (hitGameObjects[i] == hitPlayer.transform.GetChild(0).gameObject) {
                CmdPetDisplay(hitPlayer.netId);
            }
        }

        hoverObjects = hitGameObjects;
    }

    [Command]
    private void CmdPetDisplay(uint playerNetId) {
        if (NetworkServer.spawned.TryGetValue(playerNetId, out NetworkIdentity identity)) {
            Player petOwner = identity.GetComponent<Player>();
            if (petOwner != null) {
                if (isLocalPlayer) {
                    petOwner._textbox.ServerOwnPet(playerName);
                } else {
                    petOwner._textbox.ServerOtherPet(petOwner.playerName);
                }
            }
        }
    }

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
