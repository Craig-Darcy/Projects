using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manages the functionality and UI of the Journal (this is essentially an inventory/mission screen)
public class JournalMenuScript : MonoBehaviour
{
    //References to other scripts needed for functionality
    public NotesSystemScript noteSystem;
    public NoteArrayPop notesArray;
    public PageScript displayedLeft;
    public PageScript displayedRight;

    //Variables to keep track of the placement in the various arrays
    int clueNumberLeft;
    int clueNumberRight;
    int caseNumber;

    private void Start()
    {
        if (displayedRight == null)
        {
            if (noteSystem.allCluesFound == true)
            {
                displayedRight = noteSystem.hasEverything;
            }
            else if (noteSystem.allCluesFound == false)
            {
                displayedRight = noteSystem.missingClues;
            }
        }
    }
    void Update()
    {
        //Updates the clue numbers from the NotesSystemScript
        clueNumberLeft = noteSystem.currentPageLeft;
        clueNumberRight = noteSystem.currentPageRight;

        if (Input.GetKeyDown(KeyCode.J)) //Checks for the journal button to be pressed
        {
            if (noteSystem.usingNotesSystem == true) //Closes the journal if it is open
            {
                clueNumberLeft = 0;
                clueNumberRight = 1;
                noteSystem.usingNotesSystem = false;
                noteSystem.JournelClosed();
            }
            else
            {
                noteSystem.usingNotesSystem = true;
                noteSystem.JournelOpen();
            }
        }
        if (noteSystem.currentPageRight + 1 > notesArray.cases[caseNumber].clueList.Count + 1) //Checks for if the right page exceeds the number of clues in the case (removing this causes clues from other cases to be displayed incorrectly and errors when handing in the cases)
        {
            if (noteSystem.allCluesFound == true)
            {
                displayedRight = noteSystem.hasEverything;
                noteSystem.rightPage = displayedRight.pageText;
                noteSystem.notesUI.righttHeaderTextObject.text = displayedRight.pageName;
                displayedLeft = noteSystem.caseNote.clueList[clueNumberLeft];
            }
            else
            {
                displayedRight = noteSystem.missingClues;
                noteSystem.rightPage = displayedRight.pageText;
                noteSystem.notesUI.rightContentTextObject.text = noteSystem.rightPage;
                noteSystem.notesUI.righttHeaderTextObject.text = displayedRight.pageName;
                displayedLeft = noteSystem.caseNote.clueList[clueNumberLeft];
            }
        }
        else
        {
            displayedLeft = noteSystem.caseNote.clueList[clueNumberLeft];
            displayedRight = noteSystem.caseNote.clueList[clueNumberRight];
        }
        noteSystem.notesUI.leftHeaderTextObject.text = displayedLeft.pageName;
        noteSystem.notesUI.righttHeaderTextObject.text = displayedRight.pageName;
    }

    public void CaseButton(int buttonNumber)
    {
        caseNumber = buttonNumber - 1;
        switch (buttonNumber)
        {
            case 1:
                //Displays what the player has collected for the selected case
                noteSystem.usingNotesSystem = true;
                noteSystem.caseNote = notesArray.cases[caseNumber];
                noteSystem.leftPage = displayedLeft.pageText;
                noteSystem.rightPage = displayedRight.pageText;
                if (notesArray.cases[caseNumber].clueAmount <= notesArray.cases[caseNumber].clueList.Count) //Check if all clues are found for the selected case
                {
                    noteSystem.allCluesFound = true;
                }
                break;
            case 2:
                noteSystem.usingNotesSystem = true;
                noteSystem.caseNote = notesArray.cases[caseNumber];
                noteSystem.leftPage = displayedLeft.pageText;
                noteSystem.rightPage = displayedRight.pageText;
                if (notesArray.cases[caseNumber].clueAmount <= notesArray.cases[caseNumber].clueList.Count)
                {
                    noteSystem.allCluesFound = true;
                }
                break;
            case 3:
                noteSystem.usingNotesSystem = true;
                noteSystem.caseNote = notesArray.cases[caseNumber];
                noteSystem.leftPage = displayedLeft.pageText;
                noteSystem.rightPage = displayedRight.pageText;
                if (notesArray.cases[caseNumber].clueAmount <= notesArray.cases[caseNumber].clueList.Count)
                {
                    noteSystem.allCluesFound = true;
                }
                break;
        }
        NotesSystemScript.DisplayLeftPage(displayedLeft);
        NotesSystemScript.DisplayRightPage(displayedRight);
    }
    public void CloseButton()
    {
        noteSystem.JournelClosed();
    }

    public void NextButton()
    {
        if (noteSystem.currentPageRight > notesArray.cases[caseNumber].clueList.Count) // This stops the player being able to infintely scroll through the pages 
        {
            if (noteSystem.allCluesFound == true)
            {
                displayedRight = null;
            }
            else if (noteSystem.allCluesFound == false)
            {
                displayedRight = null;
            }
        }

        if (noteSystem.currentPageRight >= notesArray.cases[caseNumber].clueList.Count) // Checks if the current page is the last clue for the current case
        {
            noteSystem.notesUI.nextButton.enabled = false;
        }
        else
        {
            //Increase the left and right pages
            noteSystem.currentPageLeft += 2;
            noteSystem.currentPageRight += 2;
        }
        NotesSystemScript.DisplayLeftPage(displayedLeft);
        NotesSystemScript.DisplayRightPage(displayedRight);
    }
    public void PreviousButton()
    {
        if (noteSystem.currentPageLeft == 0) // Checks if the current page is the first one prevents infinte scrolling on the previous button (also causes weird graphical glitch)
        {
            noteSystem.notesUI.previousButton.enabled = false;
        }
        else
        {
            noteSystem.currentPageLeft -= 2;
            noteSystem.currentPageRight -= 2;
        }
        NotesSystemScript.DisplayLeftPage(displayedLeft);
        NotesSystemScript.DisplayRightPage(displayedRight);
    }
}
