using System;
using _Scripts;
using UnityEngine;

public class FarmAnimator : MonoBehaviour
{
    private Animator _farnAnim;
    [SerializeField] private SpriteRenderer _hatSpriteRenderer;
    [SerializeField] private RuntimeAnimatorController lotus;
    [SerializeField] private RuntimeAnimatorController pumpkin;
    [SerializeField] private RuntimeAnimatorController sunflower;


    private void Awake()
    {
        _farnAnim = GetComponent<Animator>();
        if (GameObject.FindWithTag("FarmManager").GetComponent<PlantTypeManager>().plantType == PlantTypeManager.PlantType.Long)
        {
            GetComponent<Animator>().runtimeAnimatorController = lotus;
        }
        else if (GameObject.FindWithTag("FarmManager").GetComponent<PlantTypeManager>().plantType == PlantTypeManager.PlantType.Medium)
        {
            GetComponent<Animator>().runtimeAnimatorController = sunflower;
        }
        else if (GameObject.FindWithTag("FarmManager").GetComponent<PlantTypeManager>().plantType == PlantTypeManager.PlantType.Short)
        {
            GetComponent<Animator>().runtimeAnimatorController = pumpkin;
        }
    }

    public void SetAnim(int state)
    {
        _farnAnim.SetInteger("AnimState", state);
    }
}