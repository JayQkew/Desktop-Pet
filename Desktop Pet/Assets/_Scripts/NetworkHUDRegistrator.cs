using System;
using Mirror;
using UnityEngine;

public class NetworkHUDRegistrator : MonoBehaviour
{
    private TransparentWindow transparentWindow;
    private NetworkManagerHUD networkHUD;

    private void Start() {
        transparentWindow = GetComponent<TransparentWindow>();
        networkHUD = GetComponent<NetworkManagerHUD>();
        
        RectTransform hudTransform = networkHUD.GetComponent<RectTransform>();
                
        if (hudTransform == null)
        {
            Canvas hudCanvas = networkHUD.GetComponentInParent<Canvas>();
            if (hudCanvas != null)
            {
                hudTransform = hudCanvas.GetComponent<RectTransform>();
            }
        }
                
        // Register the HUD RectTransform with our transparent window
        if (hudTransform != null)
        {
            transparentWindow.AddClickableUIElement(hudTransform);
            Debug.Log("Registered NetworkManagerHUD with TransparentWindow");
        }
        else
        {
            Debug.LogWarning("Could not find RectTransform for NetworkManagerHUD");
                    
            // As a fallback, try to register the entire canvas
            Canvas[] canvases = FindObjectsOfType<Canvas>();
            foreach (Canvas canvas in canvases)
            {
                RectTransform canvasRect = canvas.GetComponent<RectTransform>();
                if (canvasRect != null)
                {
                    transparentWindow.AddClickableUIElement(canvasRect);
                    Debug.Log("Registered Canvas as fallback: " + canvas.name);
                }
            }
        }
    }
}
