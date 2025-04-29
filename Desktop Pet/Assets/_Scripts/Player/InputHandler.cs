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
    
    [HideInInspector] public UnityEvent onLeftDown;
    [HideInInspector] public UnityEvent onLeftHold;
    [HideInInspector] public UnityEvent onLeftUp;
    
    [HideInInspector] public UnityEvent onRightDown;
    [HideInInspector] public UnityEvent onRightHold;
    [HideInInspector] public UnityEvent onRightUp;

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
