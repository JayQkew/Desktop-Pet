using Mirror;
using TMPro;
using UnityEngine;

public class Textbox : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI textbox;

    [Server]
    public void ServerDisplayYou(string playerName) => DisplayYou(playerName);
    
    [TargetRpc]
    public void DisplayYou(string playerName) => textbox.text = playerName;
    
    public void DisplayText(string t) => textbox.text = t;
}
