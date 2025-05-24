using System;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;

public class PetGUI : MonoBehaviour
{
    private Animator _anim;
    public Animator hatAnim;
    private PetStateManager _petStateManager;
    [SerializeField] private SpriteRenderer _hatSpriteRenderer;
    
    private int _hatSpriteID = 0;
    [SerializeField] private Sprite[] availableHats;
    
    private void Awake() {
        _anim = GetComponent<Animator>();
    }

    public void SetAnim(PetState state) {
        _anim.SetInteger("State", (int)state);
        hatAnim.SetInteger("State", (int)state);
    }
    
    public void ChangeHat(int spriteIndex) => SetHat(spriteIndex);

    public void SetHat(int spriteIndex) {
        if (spriteIndex >= 0 && (availableHats == null || spriteIndex < availableHats.Length)) {
            _hatSpriteID = spriteIndex;
            if (availableHats != null) _hatSpriteRenderer.sprite = availableHats[_hatSpriteID];
        }
    }
}
