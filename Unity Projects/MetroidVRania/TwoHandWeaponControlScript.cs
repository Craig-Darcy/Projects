using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//Manages the control of the weapon allowing it to be used in two hands
public class TwoHandWeaponControlScript : XRGrabInteractable
{
    //References to XR Rig
    XRRayInteractor xrRay1;
    XRRayInteractor xrRay2;
    XRBaseInteractor secondInteractor;

    // List of points that the object can be grabbed
    public List<XRSimpleInteractable> secondHandGrabPoints = new List<XRSimpleInteractable>();

    // Used to correct the rotation of the weapon in the players hand
    Quaternion attachInitialRotation; 

    // Enum to set the type of weapon
    public enum rotationType { None,oneHand,twoHands};
    public rotationType rotationOfGun;

    // Start is called before the first frame update
    void Start()
    {
        //Adds the grab events
        foreach (var item in secondHandGrabPoints)
        {
            item.onSelectEntered.AddListener(OnSecondHandGrab);
            item.onSelectExited.AddListener(OnSecondHandRelease);
        }
    }

    //Updates the interactable allowing for the second hand grab
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        //Adjusts gun rotation based on type
        if(secondInteractor && selectingInteractor)
        {
            selectingInteractor.attachTransform.rotation = GetRotationOfGun();
        }
        base.ProcessInteractable(updatePhase);
    }

    //Gets the gun rotation based on gun type
    private Quaternion GetRotationOfGun()
    {
        Quaternion targetRotation = default;
        switch (rotationOfGun)
        {
            case rotationType.None:
                targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position);
                break;
            case rotationType.oneHand:
                targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, selectingInteractor.attachTransform.up);
                break;
            case rotationType.twoHands:
                targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, secondInteractor.attachTransform.up);
                break;
        }
        return targetRotation;
    }

    //Grab and release events second hand to grab the object
    public void OnSecondHandGrab(XRBaseInteractor interactor)
    {
        
        secondInteractor = interactor;
        xrRay2 = interactor.GetComponent<XRRayInteractor>();
        xrRay2.allowAnchorControl = false;
    }
    public void OnSecondHandRelease(XRBaseInteractor interactor)
    {
        
        xrRay2 = interactor.GetComponent<XRRayInteractor>();
        secondInteractor = null;
        xrRay2.allowAnchorControl = false;
    }

    //Grab and release event for the game object
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        
        base.OnSelectEntered(interactor);
        attachInitialRotation = interactor.attachTransform.localRotation;
        xrRay1 = interactor.GetComponent<XRRayInteractor>();
        xrRay1.allowAnchorControl = false;
    }
    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        
        base.OnSelectExited(interactor);
        secondInteractor = null;
        interactor.attachTransform.localRotation = attachInitialRotation;
        xrRay1 = interactor.GetComponent<XRRayInteractor>();
        xrRay1.allowAnchorControl = false;
    }

    //Checks if the object is already grabbed
    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        bool isAlreadyGrabbed = selectingInteractor && !interactor.Equals(selectingInteractor);
        return base.IsSelectableBy(interactor) && !isAlreadyGrabbed;
    }
}
