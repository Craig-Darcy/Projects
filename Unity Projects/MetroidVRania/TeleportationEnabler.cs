using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

//This script sets the movement mode to the teleportation style of movement provided in Unitys XR Toolkit
public class TeleportationEnabler : MonoBehaviour
{
    public TeleportationProvider TP;
    public GameObject teleportText;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TP.enabled = true;
            teleportText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TP.enabled = false;
            teleportText.SetActive(false);
        }
    }

}
