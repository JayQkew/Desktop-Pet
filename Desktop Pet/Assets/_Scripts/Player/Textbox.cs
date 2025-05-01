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
    private void DisplayYou(NetworkConnection target) => textbox.text = "*";
    
    
    [Server]
    public void ServerDisplayOwn(NetworkConnection target, string ownerName) {
        DisplayOwnPet(target, ownerName);
    }
    
    [TargetRpc]
    private void DisplayOwnPet(NetworkConnection target, string ownerName) {
        textbox.text = $"**{ownerName}**";
    }
    
    [Server]
    public void ServerDisplayOther(NetworkConnection target, string ownerName) {
        DisplayOtherPet(target, ownerName);
    }
    
    [TargetRpc]
    private void DisplayOtherPet(NetworkConnection target, string petOwnerName) {
        textbox.text = petOwnerName;
    }
    public void DisplayText(string t) => textbox.text = t;
}
