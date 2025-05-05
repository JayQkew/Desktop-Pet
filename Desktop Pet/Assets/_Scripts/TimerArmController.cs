using System;
using UnityEngine;

public class TimerArmController : MonoBehaviour
{
    public Transform timerArm; // Assign the arm's transform
    public SpriteRenderer plantSpriteRenderer; // Assign the sprite renderer

    [Header("Plant Scriptable Objects")] 
    public Sprite planted;
    public Sprite growing;
    public Sprite finished;
    public Sprite rotten;
    private FarmAnimator _farmAnimator;
    private FarmSatateManager _farmSatateManager;

    private void Awake()
    {
        _farmAnimator = GetComponent<FarmAnimator>();
        _farmSatateManager = GameObject.FindGameObjectWithTag("FarmManager").GetComponent<FarmSatateManager>();
    }

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
                case FarmSatateManager.FarmState.Planted:
                    _farmAnimator.SetAnim(0);
                    plantSpriteRenderer.enabled = true;
                    if (timerArm != null) timerArm.gameObject.SetActive(true);
                    plantSpriteRenderer.sprite = planted;
                    Debug.Log(planted);
                    Debug.Log(plantSpriteRenderer.sprite);
                    break;
                case FarmSatateManager.FarmState.Growing:
                    _farmAnimator.SetAnim(1);
                    plantSpriteRenderer.sprite = growing;
                    Debug.Log(growing);
                    Debug.Log(plantSpriteRenderer.sprite);
                    break;
                case FarmSatateManager.FarmState.Finished:
                    _farmAnimator.SetAnim(2);
                    plantSpriteRenderer.sprite = finished;
                    Debug.Log(finished);
                    Debug.Log(plantSpriteRenderer.sprite);
                    break;
                case FarmSatateManager.FarmState.Rotten: // Or change sprite
                    _farmAnimator.SetAnim(3);
                    plantSpriteRenderer.sprite = rotten;
                    Debug.Log(rotten);
                    Debug.Log(plantSpriteRenderer.sprite);
                    new WaitForSeconds(10f);
                    _farmSatateManager.Harvest();
                    break;
                case FarmSatateManager.FarmState.Harvested:
                    plantSpriteRenderer.enabled = false;
                    if (timerArm != null) timerArm.gameObject.SetActive(false);
                    break;
            }
        }
    }
}