using System;
using Mirror;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class Player : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;

    [Space(10)]
    private GameObject _pet;

    public Textbox textbox;
    private InputHandler _inputHandler;

    public TMP_InputField inputField;
    private bool _methodSubscribed;
    public TextMeshProUGUI gameButtonText;
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
        if (inputField != null && !_methodSubscribed) {
            inputField.onValueChanged.AddListener(OnInputChange);
            _methodSubscribed = true;
        }
    }

    public override void OnStartClient() {
        base.OnStartClient();
        if (isServer) textbox.ServerDisplayYou(playerName);
        else if (isLocalPlayer) textbox.DisplayYou(playerName);
        else textbox.DisplayText("");
    }

    private void OnInputChange(string newName) {
        if (isLocalPlayer) {
            if (newName.Length > 4) {
                inputField.text = newName.Substring(0, 4);
                newName = inputField.text;
            }

            CmdUpdatePlayerName(newName);
        }
    }

    [Command]
    private void CmdUpdatePlayerName(string newName) {
        if (newName.Length <= 4) {
            playerName = newName;
            gameButtonText.text = $"{playerName} the Frog";
        }
    }

    private void OnNameChanged(string oldValue, string newValue) {
        if (gameButtonText != null) {
            gameButtonText.text = $"{playerName} the Frog";
        }

        UpdateNameDisplay();
    }

    private void UpdateNameDisplay() {
        if (isServer) textbox.ServerDisplayYou(playerName);
        else if (isLocalPlayer) textbox.DisplayYou(playerName);
        else textbox.DisplayText("");
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