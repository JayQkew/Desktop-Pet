using System;
using UnityEngine;

[Serializable]
public class FallPetState : PetBaseState
{
    [SerializeField] private Color color = Color.white;

    public override void EnterState(PetStateManager manager) {
        manager.GetComponentInChildren<SpriteRenderer>().color = color;
    }

    public override void UpdateState(PetStateManager manager) {
    }

    public override void ExitState(PetStateManager manager) {
    }
}