using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralUX : MonoBehaviour
{
    public HatButton[] hatButtons;
    public void ExitGame() => Application.Quit();

    public void SetHatButtons() {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        PetGUI[] petGUI = new PetGUI[player.Length];

        for (int i = 0; i < player.Length; i++) {
            petGUI[i] = player[i].transform.GetChild(0).GetComponentInChildren<PetGUI>();
            Debug.Log(petGUI[i]);
        }

        foreach (PetGUI gui in petGUI) {
            foreach (HatButton h in hatButtons) {
                h.petGUI = gui;
            }
        }
    }
}
