using System;
using UnityEngine;

public class HatButton : MonoBehaviour
{
    [SerializeField] private int hat;
    public PetGUI petGUI;

    public void SetHat() => petGUI.ChangeHat(hat);
}
