using Mirror;
using TMPro;
using UnityEngine;

public class Textbox : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI textbox;
    
    [Client]
    public void DisplayName(string playerName) => textbox.text = $"**{playerName}**";

    [Server]
    public void ServerDisplayYou(NetworkConnection targetClient) {
        DisplayYou(targetClient);
    }
    
    [TargetRpc]
    public void DisplayYou(NetworkConnection target) => textbox.text = "*";
    public void DisplayText(string t) => textbox.text = t;
    public void DisplayPet() => textbox.text = "*";
}
