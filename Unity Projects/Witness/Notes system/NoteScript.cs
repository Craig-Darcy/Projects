using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Creates a scriptable object that allows for cases to be made easily and attached to game objects
[CreateAssetMenu(fileName = "newNote", menuName = "Notes System/newNote")]
public class NoteScript : ScriptableObject
{
    public string noteName = string.Empty;

    public List<PageScript> clueList;

    public int caseNumber;
    public int clueAmount;
}
