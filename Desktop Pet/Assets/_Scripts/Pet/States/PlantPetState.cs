using System;
using _Scripts;
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
            // if (GameObject.FindWithTag("FarmManager").GetComponent<PlantTypeManager>().plantType == PlantTypeManager.PlantType.Long)
            // {
            //     GameObject.FindWithTag("FarmManager").GetComponent<FarmSatateManager>().PlantLong();
            // }
            // else if (GameObject.FindWithTag("FarmManager").GetComponent<PlantTypeManager>().plantType == PlantTypeManager.PlantType.Medium)
            // {
            //     GameObject.FindWithTag("FarmManager").GetComponent<FarmSatateManager>().PlantMedium();
            // }
            // else if (GameObject.FindWithTag("FarmManager").GetComponent<PlantTypeManager>().plantType == PlantTypeManager.PlantType.Short)
            // {
            //     GameObject.FindWithTag("FarmManager").GetComponent<FarmSatateManager>().PlantShort();
            // }
            // pet.CmdDestroyItem();
            plant.planted = true;
            pet.targetFood = null;
            manager.SwitchState(PetState.Idle);
        }
    }

    public override void ExitState(PetStateManager manager) {
    }
}