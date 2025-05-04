using System;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantTemp", menuName = "Scriptable Objects/PlantTemp")]
public class PlantTemp : ScriptableObject
{
    [SerializeField] private float growTime;
    
    [SerializeField] private Sprite sprite;

    private float rotTime;
    // calculate other times
    private void Awake()
    {
        rotTime = (float)(growTime * 0.1);
    }
}
