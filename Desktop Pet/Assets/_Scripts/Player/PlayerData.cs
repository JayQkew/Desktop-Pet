using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    public string playerName;
    public int playerCredits;
    public Sprite[] playerCosmetics;
}
