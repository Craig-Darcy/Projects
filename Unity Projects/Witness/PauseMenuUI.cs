using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Manages the pause menu UI and functions
public class PauseMenuUI : MonoBehaviour
{
    //Menu Variables
    public bool gamePaused = false; //Sets paused state of game
    public GameObject menuUI;
    public GameObject settingsUI;
    public basicmovement1 character; //Used to prevent player movement

    public void Resume()
    {
        //Deactivates the pause menu UI, allowing the player to move again
        menuUI.SetActive(false);
        settingsUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
        character.canMove = true;
        Cursor.visible = false;
        
    }

    void Pause()
    {
        //Activates the pause menu, preventing the player from moving
        menuUI.SetActive(true);
        settingsUI.SetActive(false);
        Time.timeScale = 0f;
        gamePaused = true;
        character.canMove = false;
        Cursor.visible = true;
        
    }

    public void Settings()
    {
        //Changes the UI to the settings menu
        menuUI.SetActive(false);
        settingsUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
        character.canMove = false;
    }

    public void MainMenu()
    {
        //Brings player back to the main menu
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Back()
    {
        //Brings player back to the main menu
        menuUI.SetActive(true);
        settingsUI.SetActive(false);
        Time.timeScale = 0f;
        gamePaused = true;
        character.canMove = false;
    }

    void Update()
    {
        //Checks if the esc key is pressed to pause the game
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            
            if(gamePaused)
            {
                Resume();
            }

            else
            {
                Pause();
            }

        }
    }
}
