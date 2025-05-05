using System;
using UnityEngine;

public class HatButton : MonoBehaviour
{
    [SerializeField] private Sprite hat;
    public PetGUI petGUI;

    public void SetHat() => petGUI.SetHatSprite(hat);
}
