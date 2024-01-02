using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Derived class of State, this is the logic for the idle state
public class AIIdle : State
{
    //Other states in the state machine
    public AIChase chaseState;

    //Enemy Variables
    public bool canSee;
    public float idleSpeed;
    public float waitTimer;
    public float startTimer;

    public Transform[] patrolLocations; //Empty game objects placed for the AI to move towards
    private int randomLocation;

    void Start()
    {
        waitTimer = startTimer;
        randomLocation = Random.Range(0, patrolLocations.Length);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolLocations[randomLocation].position, idleSpeed * Time.deltaTime); //Move towards patrol location

        if (Vector3.Distance(transform.position, patrolLocations[randomLocation].position) < 0.5f) //Check for patrol location
        {
            if (waitTimer <= 0)
            {
                randomLocation = Random.Range(0, patrolLocations.Length);
                waitTimer = startTimer;
            }
            else
            {
                waitTimer -= Time.deltaTime;
            }
        }

    }
    public override State RunCurrent()
    {
        if (canSee)
        {
            return chaseState;
        }
        else
        {
            return this;
        }
    }
}
