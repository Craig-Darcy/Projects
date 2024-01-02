using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manages the population of the notes array on the pick up of the 
public class NoteArrayPop : MonoBehaviour
{
    public NoteScript[] cases;
    public int caseNumberInt;
    public int numOfCases;
    public NoteScript notePickUp;

    // Start is called before the first frame update
    void Start()
    {
        cases = new NoteScript[numOfCases];
    }

    // Update is called once per frame
    void Update()
    {
        PopulateArray();
    }

    public void PopulateArray()
    {
        caseNumberInt = notePickUp.caseNumber - 1;
        for (int i = 0; i < numOfCases; i++)
        {
            cases[caseNumberInt] = notePickUp;
        }
    }
}
