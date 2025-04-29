using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantTemp", menuName = "Scriptable Objects/PlantTemp")]
public class PlantTemp : ScriptableObject
{
    [SerializeField] private string plantName;

    [SerializeField] private float growTime;
    
    [SerializeField] private Sprite sprite;

    // public File file;
    // calculate other times
}
