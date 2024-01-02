using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StatblockScript : MonoBehaviour
{
    // Player stats
    public float playerHealth = 100.0f;
    public int playerSanity = 100;

    basicmovement1 movement; //Allows freezing of movement

    public GameObject[] sanityShown = GameObject.FindGameObjectsWithTag("Sprite"); //Array for sanity meter sprites

    public int stage; //Current stage of sanity

    void Update()
    {
        if (playerHealth <= 0) //Loads game over screen
        {
            movement.StopMove();
            SceneManager.LoadScene("loss screen");
        }

        //Sets min and max sanity
        if(playerSanity < 0)
        {
            playerSanity = 0;
        }
        if(playerSanity > 100)
        {
            playerSanity = 100;
        }

        //Hides all sanity sprites until current stage is determined
        foreach (GameObject sprite in sanityShown)
        {
            sprite.SetActive(false);
        }

        // Determines the sanity stage and sets the appropriate sprite
        if (playerSanity >= 0 && playerSanity <= 20)
        {
            stage = 7;
            sanityShown[stage].SetActive(true);
        }
  
        if (playerSanity > 20 && playerSanity <= 40)
        {
            stage = 6;
            sanityShown[stage].SetActive(true);
        }
             
        if (playerSanity > 40 && playerSanity <= 55)
        {
            stage = 5;
            sanityShown[stage].SetActive(true);
        }
           
        if (playerSanity > 55 && playerSanity <= 70)
        {
            stage = 4;
            sanityShown[stage].SetActive(true);
        }
            
        if (playerSanity > 70 && playerSanity <= 80)
        {
            stage = 3;
            sanityShown[stage].SetActive(true);
        }
            
        if (playerSanity > 80 && playerSanity <= 90)
        {
            stage = 2;
            sanityShown[stage].SetActive(true);
        }
           
        if (playerSanity > 90 && playerSanity <= 95)
        {
            stage = 1;
            sanityShown[stage].SetActive(true);
        }
           
        if (playerSanity > 95 && playerSanity <= 100)
        {
             stage = 0;
             sanityShown[stage].SetActive(true);
        }

           
    }

}
