using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class PlantPetState : PetBaseState
{
    public Plant plant;
    private Pet pet;

    private float currTime;
    public override void EnterState(PetStateManager manager) {
        pet ??= manager.GetComponent<Pet>();
        plant = pet.targetFood.GetComponent<Plant>();
        plant.GetComponent<Rigidbody2D>().linearVelocityX = 0;
        manager.walkState.targetPosition = plant.transform.position;

        currTime = 0;
        plant.canInteract = false;
    }

    public override void UpdateState(PetStateManager manager) {
        currTime += Time.deltaTime;
        if (currTime >= 7) {
            pet.CmdDestroyItem();
            manager.CmdSwitchState(PetState.Idle);
        }
    }

    public override void ExitState(PetStateManager manager) {
    }
}