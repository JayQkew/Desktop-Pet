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
        CmdCheckHover();
    }

    public override void OnStartClient() {
        base.OnStartClient();
        if(isLocalPlayer) CmdDisplayYou();
        else textbox.DisplayText("");
    }

    [Command]
    private void CmdDisplayYou() {
        textbox.ServerDisplayYou();
    }
    
    private void Hover() {
        if(!isLocalPlayer) return;
        
        CmdCheckHover();
    }

    [Command]
    private void CmdCheckHover() {
        Collider2D hit = Physics2D.OverlapPoint(_inputHandler.mousePos, hoverLayer);

        if(hit == null) {
            textbox.ServerDisplayYou();
            return;
        }
        
        GameObject hitGameObject = hit.gameObject;
        Player hitPlayer = hitGameObject.GetComponentInParent<Player>();

        if (hitGameObject == transform.GetChild(0).gameObject) {
            if(isLocalPlayer) textbox.ServerOwnPet(playerName);
            else hitPlayer.textbox.ServerOtherPet(hitPlayer.playerName);
        }
        else  hitPlayer.textbox.ServerOtherPet(hitPlayer.playerName);
        
        // GameObject[] hitGameObjects = new GameObject[hit.Length];
        // for (int i = 0; i < hit.Length; i++) {
        //
        //     if (hitPlayer == null) continue;
        //     
        //     if (hitGameObject == hitPlayer.transform.GetChild(0).gameObject) {
        //         if (isLocalPlayer) {
        //             hitPlayer._textbox.ServerOwnPet(playerName);
        //         }
        //         else {
        //             hitPlayer._textbox.ServerOtherPet(hitPlayer.playerName);
        //         }
        //     }
        // }
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
