using Mirror;
using TMPro;
using UnityEngine;

public class Textbox : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI textbox;

    [Server]
    public void ServerDisplayYou() {
        DisplayYou();
    }
    
    [TargetRpc]
    private void DisplayYou() => textbox.text = "*";

    [Server]
    public void ServerOwnPet(string petName) {
        DisplayOwnPet(petName);
    }
    
    [TargetRpc]
    private void DisplayOwnPet(string petName) {
        textbox.text = $"**{petName}**";
    }

    [Server]
    public void ServerOtherPet(string petName) {
        DisplayOtherPet(petName);
    }
    
    [TargetRpc]
    private void DisplayOtherPet(string petName) {
        textbox.text = petName;
    }
    public void DisplayText(string t) => textbox.text = t;
}
