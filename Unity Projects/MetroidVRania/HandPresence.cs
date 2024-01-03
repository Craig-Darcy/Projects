using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    #region Variables for hands

    public bool showController = false;
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;
    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;
    private Animator handAnimator;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        TryInitialize();
    }

    //Attempts to initialize the hand presence based on the input device
    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>(); //Available input devices

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices); //Gets devices with specific characteristics

        //Checks for at least one input device
        if (devices.Count > 0)
        {
            targetDevice = devices[0]; //Sets target to the first available device

            //Finds the corresponding controller prefab
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.Log("Did not find corresponding controller model");
            }

            // Instantiates the hand model and the animator for the hand model
            spawnedHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();
        }
    }

    //Updates the hand animations based on what is pressed
    void UpdateHandAnimation()
    {
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!targetDevice.isValid)//Trys to initialize the device again if it failed
        {
            TryInitialize();
        }
        else
        {
            if (showController) //Chooses to show the controller or the hand model
            {
                if(spawnedHandModel)
                    spawnedHandModel.SetActive(false);
                if(spawnedController)
                    spawnedController.SetActive(true);
            }
            else
            {
                if (spawnedHandModel)
                    spawnedHandModel.SetActive(true);
                if (spawnedController)
                    spawnedController.SetActive(false);
                UpdateHandAnimation();
            }
        }
    }
}
