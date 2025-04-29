using System;
using UnityEngine;

[Serializable]
public class PlantPetState : PetBaseState
{
    [SerializeField] private Color color = Color.white;
    public override void EnterState(PetStateManager manager) {
        Debug.Log("Pet started planting something.");
        manager.GetComponentInChildren<SpriteRenderer>().color = color;
    }

    public override void UpdateState(PetStateManager manager) {
    }

    public override void ExitState(PetStateManager manager) {
        Debug.Log("Pet finished planting.");
    }
}