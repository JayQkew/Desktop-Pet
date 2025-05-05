using System;
using UnityEngine;

public class PetGUI : MonoBehaviour
{
    private Animator _anim;
    private PetStateManager _petStateManager;
    [SerializeField] private SpriteRenderer _hatSpriteRenderer;
    

    private void Awake() {
        _anim = GetComponent<Animator>();
    }

    public void SetAnim(PetState state) => _anim.SetInteger("State", (int)state);
    
    public void SetHatSprite(Sprite sprite) => _hatSpriteRenderer.sprite = sprite;
}
