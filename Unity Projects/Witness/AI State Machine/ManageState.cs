using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script manages the behaviour of the state machine and is played on the AI enemies
public class ManageState : MonoBehaviour
{
    public State currentState; //Current state of the Enemy

    void Update()
    {
        RunStateMachine();
    }
    void RunStateMachine()
    {
        State nextState = currentState?.RunCurrent(); //Gets the next state by running the current ones logic

        if(nextState != null)
        {
            SwitchToNext(nextState);
        }

    }
    void SwitchToNext(State state)
    {
        currentState = state;
    }
}
