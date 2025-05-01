using System;
using UnityEngine;

[Serializable]
public class WorkPetState : PetBaseState
{
    [SerializeField] private Sprite sprite;
    public override void EnterState(PetStateManager manager) {
        Debug.Log("Pet started working.");
        manager.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
    }

    public override void UpdateState(PetStateManager manager) {
    }

    public override void ExitState(PetStateManager manager) {
        Debug.Log("Pet finished working and earned a treat!");
    }
}