using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class ActiveSwitch : MonoBehaviour
{
   public List<GameObject> gameObjects;

   public void SwitchActiveState()
   {
      if (gameObjects != null)
      {
         foreach (GameObject go in gameObjects)
         {
            go.SetActive(!go.activeSelf);
         }
      }
      else
      {
         Debug.LogError(gameObject.name + " does not have game objects in the ActiveSwitch.cs Script");
      }
   }
}
