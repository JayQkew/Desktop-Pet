using System;
using UnityEngine;

[Serializable]
public class PetPetState : PetBaseState
{
    [SerializeField] private Color color = Color.white;
    [SerializeField] private Sprite sprite;
    public override void EnterState(PetStateManager manager) {
        Debug.Log("Pet is being petted. Purrrr...");
        manager.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
    }

    public override void UpdateState(PetStateManager manager) {
    }

    public override void ExitState(PetStateManager manager) {
        Debug.Log("Petting session ended.");
    }
}