using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Manages the pick up interaction for the clue objects
public class CluePickUp : MonoBehaviour
{
    //Checks for the clue
    public bool add;
    public bool autoDisplay = false; //Some clues in the game should be shown the the player immediately for story reasons

    //References to other scripts that are required for functionality
    public NoteScript note; //Used when the player starts the actual mission making a note showing the mission description
    public PageScript page; //Used when a clue in the case is picked up adding a page for the player to see if they are on the right track
    public NoteArrayPop pickUp;
    public NotesSystemScript checker;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                add = true;
            }
            if (add)
            {
                if(gameObject.tag == "Case") //Adds the case to the players journal
                {
                    pickUp.notePickUp = note;
                    checker.caseNote = note;
                }
                if(gameObject.tag == "Clue") //Adds the clue to the case in the players journal
                {
                    checker.AddPage(page);
                }
                Destroy(gameObject);
            }
        }
    }
}