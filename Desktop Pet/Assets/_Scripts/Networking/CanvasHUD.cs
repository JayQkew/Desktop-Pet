using System;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class CanvasHUD : MonoBehaviour
{
    public Button hostBtn;

    private void Start() {
        hostBtn.onClick.AddListener(Host);
    }

    public void Host() {
        NetworkManager.singleton.StartHost();
    }
}
