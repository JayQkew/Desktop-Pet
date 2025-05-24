using Mirror;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public string playerName;

    [Space(10)]
    private GameObject _pet;
    private InputHandler _inputHandler;

    [SerializeField] private GameObject heldObject;
    private IInteractable _interactable;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private LayerMask hoverLayer;
    private Vector2 offset;

    private void Awake() {
        _pet = transform.GetChild(0).gameObject;
        _inputHandler = GetComponent<InputHandler>();
    }

    private void Start() {
        _inputHandler.onLeftDown.AddListener(LeftDown);
        _inputHandler.onLeftHold.AddListener(LeftHold);
        _inputHandler.onLeftUp.AddListener(LeftUp);

        _inputHandler.onRightDown.AddListener(RightDown);
        _inputHandler.onRightHold.AddListener(RightHold);
        _inputHandler.onRightUp.AddListener(RightUp);
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