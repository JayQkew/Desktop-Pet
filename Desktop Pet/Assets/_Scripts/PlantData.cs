using UnityEngine;

[CreateAssetMenu(fileName = "PlantData", menuName = "Farming/Plant Data", order = 0)]
public class PlantData : ScriptableObject
{
    public string plantName;
    public float growthTimeSeconds;
    public Sprite planted;
    public Sprite growing;
    public Sprite finished;
    public Sprite rotten;
    // Add other plant-specific data like money earned.
}