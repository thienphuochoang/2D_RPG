using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen_UI : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void FadeOut() => _animator.SetTrigger("FadeOut");
    public void FadeIn() => _animator.SetTrigger("FadeIn");
}
