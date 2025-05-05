using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralUX : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI gameButtonText;
    public HatButton[] hatButtons;
    public void ExitGame() => Application.Quit();

    public void SetInputField() {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in player) {
            p.GetComponent<Player>().inputField = inputField;
        }
    }

    public void SetButtonName() {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in player) {
            p.GetComponent<Player>().gameButtonText = gameButtonText;
        }
    }

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
