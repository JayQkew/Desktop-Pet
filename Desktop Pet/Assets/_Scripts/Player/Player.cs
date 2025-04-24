using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private InputHandler _inputHandler;
    [SerializeField] private GameObject heldObject;
    private IInteractable _interactable;
    [SerializeField] private LayerMask interactableLayer;
    private Vector2 offset;

    private void Awake() {
        _inputHandler = GetComponent<InputHandler>();
    }

    private void Start() {
        _inputHandler.onLeftDown.AddListener(Click);
        _inputHandler.onLeftHold.AddListener(Hold);
        _inputHandler.onLeftUp.AddListener(Release);
    }

    private void Click() {
        Collider2D hit = Physics2D.OverlapPoint(_inputHandler.mousePos, interactableLayer);
        if (hit != null) {
            heldObject = hit.gameObject;
            _interactable = hit.GetComponent<IInteractable>();
            offset = (Vector2)heldObject.transform.position - _inputHandler.mousePos;
            
            _interactable.OnPickup();
        }
    }

    private void Hold() {
        if (_interactable != null) {
            _interactable.OnHeld(offset + _inputHandler.mousePos);
        }
    }

    private void Release() {
        if (_interactable != null) {
            _interactable.OnDrop();

            heldObject = null;
            _interactable = null;
        }
    }
}
