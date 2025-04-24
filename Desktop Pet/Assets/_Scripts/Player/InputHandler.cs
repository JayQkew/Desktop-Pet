using System;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Mirror.Examples.BilliardsPredicted.PlayerInput;

public class InputHandler : MonoBehaviour
{
    public PlayerInput playerInput;
    
    public Vector2 mousePos;
    public bool leftClick;
    public bool rightClick;

    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
    }

    public void MousePos(InputAction.CallbackContext ctx) {
        mousePos = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
    }

    public void LeftClick(InputAction.CallbackContext ctx) {
        if (ctx.performed) leftClick = true;
        else if (ctx.canceled) leftClick = false;
    }
    
    public void RightClick(InputAction.CallbackContext ctx) {
        if (ctx.performed) rightClick = true;
        else if (ctx.canceled) rightClick = false;
    }
}
