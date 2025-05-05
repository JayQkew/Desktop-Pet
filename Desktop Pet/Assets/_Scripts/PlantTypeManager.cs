using System;
using UnityEngine;

namespace _Scripts
{
    public class PlantTypeManager : MonoBehaviour
    {
        public enum PlantType
        {
            Long,
            Medium,
            Short,
            none
        }

        public PlantType plantType;
        
        private void Awake()
        {
            plantType = PlantType.none;
        }
        
        public void LongPlant()
        {
            plantType = PlantType.Long;
        }

        public void ShortPlant()
        {
            plantType = PlantType.Short;
        }

        public void MediumPlant()
        {
            plantType = PlantType.Medium;
        }
    }
}