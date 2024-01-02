using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Abstract class for the generic state of the state machine
public abstract class State : MonoBehaviour
{
    //Must be implemented by the derived class, returns the next state to be run
    public abstract State RunCurrent();
}
