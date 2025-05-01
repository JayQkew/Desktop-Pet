using System;
using UnityEngine;

public class PetGUI : MonoBehaviour
{
    private Animator _anim;

    private void Awake() {
        _anim = GetComponent<Animator>();
    }

    public void SetAnim(PetState state) => _anim.SetInteger("State", (int)state);
}
