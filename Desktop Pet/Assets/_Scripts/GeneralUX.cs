using TMPro;
using UnityEngine;

public class GeneralUX : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI gameButtonText;
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
}
