using Mirror;
using UnityEngine;

[SelectionBase]
public class Poop : NetworkBehaviour, IInteractable
{
    public override void OnStartClient() {
        base.OnStartClient();
    }

    [Command(requiresAuthority = false)]
    private void CmdDestroyPoop() {
        NetworkServer.Destroy(gameObject);
    }
    
    public void OnLeftPickup() => CmdDestroyPoop();

    public void OnLeftDrop() {
    }

    public void OnLeftHeld(Vector2 offset) {
    }

    public void OnRightPickup() {
    }

    public void OnRightDrop() {
    }

    public void OnRightHeld(Vector2 offset) {
    }
}
