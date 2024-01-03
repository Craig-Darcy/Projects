using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// This script is used to turn off the hand models when the player is holding an object
public class HandVisabilityScript : XRDirectInteractor
{
    private SkinnedMeshRenderer handRenderer = null;
    protected override void Awake()
    {
        base.Awake();
        handRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public void VisibilitySetting(bool factor)
    {
        handRenderer.enabled = factor;
    }
}
