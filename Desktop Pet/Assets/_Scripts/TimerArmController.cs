using UnityEngine;

public class TimerArmController : MonoBehaviour
{
    public Transform timerArm; // Assign the arm's transform
    public SpriteRenderer plantSpriteRenderer; // Assign the sprite renderer

    [Header("Plant Scriptable Objects")] 
    public Sprite growing;
    public Sprite finished;
    public Sprite rotten;
    
    public void SetFarmState(FarmSatateManager.FarmState state)
    {
        if (plantSpriteRenderer != null)
        {
            switch (state)
            {
                case FarmSatateManager.FarmState.Empty:
                    plantSpriteRenderer.enabled = false;
                    if (timerArm != null) timerArm.gameObject.SetActive(false);
                    break;
                case FarmSatateManager.FarmState.Growing:
                    plantSpriteRenderer.enabled = true;
                    if (timerArm != null) timerArm.gameObject.SetActive(true);
                    plantSpriteRenderer.sprite = growing;
                    Debug.Log(growing);
                    Debug.Log(plantSpriteRenderer.sprite);
                    break;
                case FarmSatateManager.FarmState.Finished:
                    plantSpriteRenderer.sprite = finished;
                    Debug.Log(finished);
                    Debug.Log(plantSpriteRenderer.sprite);
                    break;
                case FarmSatateManager.FarmState.Rotten: // Or change sprite
                    plantSpriteRenderer.sprite = rotten;
                    Debug.Log(rotten);
                    Debug.Log(plantSpriteRenderer.sprite);
                    break;
                case FarmSatateManager.FarmState.Harvested:
                    plantSpriteRenderer.enabled = false;
                    if (timerArm != null) timerArm.gameObject.SetActive(false);
                    break;
            }
        }
    }
}