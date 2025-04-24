using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using PlayerInput = Mirror.Examples.BilliardsPredicted.PlayerInput;

public class InputHandler : MonoBehaviour
{
    public Vector2 mousePos;
    public bool leftClick;
    public bool rightClick;
    
    public UnityEvent onLeftDown;
    public UnityEvent onLeftHold;
    public UnityEvent onLeftUp;
    
    public UnityEvent onRightDown;
    public UnityEvent onRightHold;
    public UnityEvent onRightUp;

    private void Update() {
        if (leftClick) onLeftHold?.Invoke();
        if (rightClick) onRightHold?.Invoke();
    }

    public void MousePos(InputAction.CallbackContext ctx) {
        mousePos = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
    }

    public void LeftClick(InputAction.CallbackContext ctx) {
        if (ctx.performed) {
            leftClick = true;
            onLeftDown?.Invoke();
        }
        else if (ctx.canceled) {
            leftClick = false;
            onLeftUp?.Invoke();
        }
    }
    
    public void RightClick(InputAction.CallbackContext ctx) {
        if (ctx.performed) {
            rightClick = true;
            onRightDown?.Invoke();
        }
        else if (ctx.canceled) {
            rightClick = false;
            onRightUp?.Invoke();
        }
    }
}
