using UnityEngine;

[CreateAssetMenu(fileName = "PlantData", menuName = "Farming/Plant Data", order = 0)]
public class PlantData : ScriptableObject
{
    public string plantName;
    public float growthTimeSeconds;
    // Add other plant-specific data like harvest yield, etc.
}