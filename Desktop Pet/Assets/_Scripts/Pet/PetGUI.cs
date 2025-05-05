using System;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;

public class PetGUI : NetworkBehaviour
{
    private Animator _anim;
    public Animator hatAnim;
    private PetStateManager _petStateManager;
    [SerializeField] private SpriteRenderer _hatSpriteRenderer;
    
    [SyncVar(hook = nameof(OnHatSpriteChanged))]
    private int _hatSpriteID = 0;
    [SerializeField] private Sprite[] availableHats;
    
    private void Awake() {
        _anim = GetComponent<Animator>();
    }

    public void SetAnim(PetState state) {
        _anim.SetInteger("State", (int)state);
        hatAnim.SetInteger("State", (int)state);
    }
    
    public void ChangeHat(int spriteIndex)
    {
        if (isLocalPlayer)
        {
            CmdSetHatSprite(spriteIndex);
        }
        else
        {
            if (spriteIndex >= 0 && (availableHats == null || spriteIndex < availableHats.Length))
            {
                _hatSpriteID = spriteIndex;
            }
        }
    }
    
    [Command]
    public void CmdSetHatSprite(int spriteIndex)
    {
        SetHat(spriteIndex);
    }

    [Server]
    public void SetHat(int spriteIndex) {
        if (spriteIndex >= 0 && (availableHats == null || spriteIndex < availableHats.Length))
        {
            _hatSpriteID = spriteIndex;
        }
    }
    private void OnHatSpriteChanged(int oldSpriteID, int newSpriteID)
    {
        if (newSpriteID >= 0 && availableHats != null && newSpriteID < availableHats.Length)
        {
            _hatSpriteRenderer.sprite = availableHats[newSpriteID];
        }
        else
        {
            _hatSpriteRenderer.sprite = null;
        }
    }
}
