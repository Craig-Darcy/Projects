using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

//Struct to define the UI of the notes system
[Serializable()]
public struct ElementsOfNotesUI
{
    //Text Objects
    public TextMeshProUGUI leftContentTextObject;
    public TextMeshProUGUI rightContentTextObject;
    public TextMeshProUGUI leftHeaderTextObject;
    public TextMeshProUGUI righttHeaderTextObject;
    public TextMeshProUGUI leftEasyReaderTextObject;
    public TextMeshProUGUI rightEasyReaderTextObject;

    //Canvas Groups
    public CanvasGroup easyReaderGroup;
    public CanvasGroup notesMenuGroup;
    public CanvasGroup notesSelectedGroup;
    public CanvasGroup readButton;
    public CanvasGroup nextButton;
    public CanvasGroup previousButton;


    //Image Objects
    public Image leftPage;
    public Image rightPage;

}

// Manages the UI and functionality of the notes system
public class NotesSystemScript : MonoBehaviour
{

    public ElementsOfNotesUI notesUI = new ElementsOfNotesUI();
    public bool usingNotesSystem;

    //References to scripts needed for functionality
    public PageScript missingClues;
    public PageScript hasEverything;
    public JournalMenuScript menuControl;
    public basicmovement1 character;
    public FPSMouse mouseMove;
    public NoteScript caseNote;

    //Notes system variables
    public bool allCluesFound;
    public int currentPageLeft;
    public int currentPageRight;
    public string leftPage;
    public string rightPage;

    //Textures for the left and right pages
    public Sprite defaultTextureLeft;
    public Sprite defaultTextureRight;

    //Actions for updating the displayed pages
    public static Action<PageScript> displayLeftAction = delegate { };
    public static Action<PageScript> displayRightAction = delegate { };


    private void OnEnable()
    {
        //Sets the display actions
        displayLeftAction += DisplayLeftPageUpdate;
        displayRightAction += DisplayRightPageUpdate;
    }
    private void OnDisable()
    {
        // Removes the display actions
        displayLeftAction -= DisplayLeftPageUpdate;
        displayRightAction -= DisplayRightPageUpdate;
    }
    void Start()
    {
        JournelClosed();
        notesUI.leftPage.sprite = defaultTextureLeft;
        notesUI.rightPage.sprite = defaultTextureRight;
    }

    // Update is called once per frame
    void Update()
    {
        currentPageRight = currentPageLeft + 1; //Sets the current right page based on the left page

    }

    public void JournelOpen()
    {
        UpdateCanvasGroup(true, notesUI.notesMenuGroup);
        character.canMove = false;
        mouseMove.StopMouseMove();
        Cursor.visible = true;
        Time.timeScale = 0;
    }
    public void JournelClosed()
    {
        UpdateCanvasGroup(false, notesUI.notesMenuGroup);
        UpdateCanvasGroup(false, notesUI.notesSelectedGroup);
        character.canMove = true;
        mouseMove.AllowMouseMove();
        Cursor.visible = false;
        Time.timeScale = 1;
    }


    public void UpdateCanvasGroup(bool state, CanvasGroup canvasGroup) //Updates the canvas groups
    {
        if (state == true)
        {
            canvasGroup.alpha = 1.0f;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }
        else
        {
            canvasGroup.alpha = 0.0f;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }

    // Displays left and right pages based on page provided
    public static void DisplayLeftPage(PageScript page)
    {
        displayLeftAction(page);
    }
    public static void DisplayRightPage(PageScript page)
    {
        displayRightAction(page);
    }

    //Updates the left and right page
    public void DisplayLeftPageUpdate(PageScript page)
    {
        if (page == null)
        {
            return;
        }
        else
        {
            UpdateCanvasGroup(true, notesUI.notesSelectedGroup);
            leftPage = caseNote.clueList[currentPageLeft].pageText;
            notesUI.leftContentTextObject.text = leftPage;
            notesUI.leftHeaderTextObject.text = page.pageName;
        }
    }
    public void DisplayRightPageUpdate(PageScript page)
    {
        if (page == null)
        {
            return;
        }
        else
        {
            rightPage = caseNote.clueList[currentPageRight].pageText;
            UpdateCanvasGroup(true, notesUI.notesSelectedGroup);
            notesUI.rightContentTextObject.text = rightPage;
            notesUI.righttHeaderTextObject.text = page.pageName;
        }
    }

    public void AddPage(PageScript page)
    {
        caseNote.clueList.Add(page);
    }

}
