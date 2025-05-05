using System;
using System.Collections;
using UnityEngine;

public class FarmSatateManager : MonoBehaviour
{
    [Header("Plant Scriptable Objects")]
    public PlantData shortPlantData;
    public PlantData mediumPlantData;
    public PlantData longPlantData;

    [Header("Farm State")]
    public FarmState farmState = FarmState.Empty;
    public PlantType plantType = PlantType.None;
    [SerializeField]private float currentTimer = 0f;   
    [SerializeField] private float plantingTime = 5f;
    [SerializeField]private float growingTime = 0f;
    [SerializeField]private float finishedTime = 5f; // Example time before rotting

    [Header("Prefab References")]
    public GameObject farmPrefab;
    public GameObject timerArmPrefab;

    private GameObject currentFarmInstance;
    private TimerArmController currentTimerArmController;

    public enum FarmState
    {
        Empty,
        Growing,
        Finished,
        Rotten,
        Harvested,
        Planted
    }

    public enum PlantType
    {
        None,
        ShortFlower,
        MediumFlower,
        LongFlower
    }

    private void Start()
    {
        UpdateFarmState(); // Initial setup
    }

    private void Update()
    {
        if (farmState == FarmState.Planted)
        {
            currentTimer += Time.deltaTime;
            if (currentTimer >= plantingTime)
            {
                farmState = FarmState.Finished;
                currentTimer = 0f;
                UpdateFarmState();
            }
        }
        else if (farmState == FarmState.Growing)
        {
            currentTimer += Time.deltaTime;
            if (currentTimer >= growingTime)
            {
                farmState = FarmState.Finished;
                currentTimer = 0f;
                UpdateFarmState();
            }
        }
        else if (farmState == FarmState.Finished)
        {
            currentTimer += Time.deltaTime;
            if (currentTimer >= finishedTime)
            {
                farmState = FarmState.Rotten;
                UpdateFarmState();
            }
        }
    }

    private void UpdateFarmState()
    {
        if (currentTimerArmController != null)
        {
            currentTimerArmController.SetFarmState(farmState);
        }

        switch (farmState)
        {
            case FarmState.Empty:
                // In TimerArmController
                break;
            case FarmState.Planted:
                // In TimerArmController, handle visual progression
                break;
            case FarmState.Growing:
                // In TimerArmController, handle visual progression
                break;
            case FarmState.Finished:
                // In TimerArmController, show finished state
                break;
            case FarmState.Rotten:
                // In TimerArmController, show rotten state
                break;
            case FarmState.Harvested:
                // Handled in Harvest() method
                break;
        }
    }

    public void PlantShort()
    {
        PlantCrop(PlantType.ShortFlower, 
            shortPlantData.growthTimeSeconds,
            shortPlantData.planted,
            shortPlantData.growing, 
            shortPlantData.finished, 
            shortPlantData.rotten);
    }

    public void PlantMedium()
    {
        PlantCrop(PlantType.MediumFlower, 
            mediumPlantData.growthTimeSeconds,
            mediumPlantData.planted,
            mediumPlantData.growing, 
            mediumPlantData.finished, 
            mediumPlantData.rotten);    
    }

    public void PlantLong()
    {
        PlantCrop(PlantType.LongFlower, 
            longPlantData.growthTimeSeconds,
            longPlantData.planted,
            longPlantData.growing, 
            longPlantData.finished, 
            longPlantData.rotten);
    }

    private void PlantCrop(PlantType type, float growthTime, Sprite planted, Sprite growing, Sprite finished, Sprite rotten)
    {
        if (farmState == FarmState.Empty)
        {
            plantType = type;
            growingTime = growthTime;
            farmState = FarmState.Growing;
            currentTimer = 0f;

            // Instantiate Farm Prefab
            GameObject player = GameObject.FindGameObjectWithTag("Pet");
            Debug.Log(player.transform.position);
            if (player != null)
            {
                currentFarmInstance = Instantiate(farmPrefab, new Vector2(player.transform.position.x, player.transform.position.y + 0.25f), Quaternion.identity);
                // Instantiate Timer Arm Prefab and attach it to the farm
                if (timerArmPrefab != null && currentFarmInstance != null)
                {
                    GameObject timerArmInstance = Instantiate(timerArmPrefab, currentFarmInstance.transform.position, Quaternion.identity, currentFarmInstance.transform);
                    timerArmInstance.GetComponent<TimerArmController>().planted = planted;
                    timerArmInstance.GetComponent<TimerArmController>().growing = growing;
                    timerArmInstance.GetComponent<TimerArmController>().finished = finished;
                    timerArmInstance.GetComponent<TimerArmController>().rotten = rotten;
                    currentTimerArmController = timerArmInstance.GetComponent<TimerArmController>();
                    if (currentTimerArmController != null)
                    {
                        currentTimerArmController.SetFarmState(farmState);
                    }
                    else
                    {
                        Debug.LogError("Timer Arm Prefab does not have a TimerArmController script attached!");
                    }
                }
                else
                {
                    Debug.LogError("Timer Arm Prefab is null or Farm Instance is null!");
                }
            }
            else
            {
                Debug.LogError("Player with tag 'Player' not found!");
            }

            UpdateFarmState();
        }
        else
        {
            Debug.Log("Farm is not empty. Cannot plant.");
        }
    }

    public void Harvest()
    {
        if (farmState == FarmState.Rotten)
        {
            // Give the player plants
            Debug.Log("Harvested " + plantType);

            farmState = FarmState.Empty;
            plantType = PlantType.None;
            currentTimer = 0f;
            growingTime = 0f;

            if (currentTimerArmController != null)
            {
                currentTimerArmController.SetFarmState(farmState);
            }

            UpdateFarmState();
        }
        else if (farmState == FarmState.Rotten)
        {
            Debug.Log("Crop is rotten. Cannot harvest.");
            farmState = FarmState.Empty;
            plantType = PlantType.None;
            currentTimer = 0f;
            growingTime = 0f;
            if (currentTimerArmController != null)
            {
                currentTimerArmController.SetFarmState(farmState);
            }
            UpdateFarmState();
        }
        else if (farmState == FarmState.Empty)
        {
            Debug.Log("Farm is empty. Nothing to harvest.");
        }
        else if (farmState == FarmState.Growing)
        {
            Debug.Log("Crop is still growing.");
        }
    }
}