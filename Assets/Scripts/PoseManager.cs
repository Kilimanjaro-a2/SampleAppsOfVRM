﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseManager : MonoBehaviour
{
    [SerializeField] protected List<Animator> _animators;
    
    public void OnAnimationButtonPressed(string poseName)
    {
        foreach(var anim in _animators)
        {
            anim.SetTrigger(poseName);
        }
    }
}
