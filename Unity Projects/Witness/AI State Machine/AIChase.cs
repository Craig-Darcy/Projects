using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Derived class of State, this is the logic for the chase state
public class AIChase : State
{
    //Other states in the state machine
    public AIAttack attackState;
    public AIIdle returnToIdle;

    //Player Variables
    public bool isInRange;
    public Transform player;

    //Enemy Variables
    public float attackRange;
    public float chaseSpeed;

    private void Update()
    {

        if (attackState.hasAttacked == true)//Resets isInRange
        {
            attackState.hasAttacked = false;
            isInRange = false;
        }

        if (returnToIdle.canSee)//changes state if the player is visable
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else
        {
            player = null;
        }

        if (Vector3.Distance(transform.position, player.position) > attackRange)//Move towards player if outside of attack range
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
            isInRange = false;
        }
        else if (Vector3.Distance(transform.position, player.position) < attackRange) //stops moving to attack
        {
            transform.position = this.transform.position;
            isInRange = true;
        }
    }
    public override State RunCurrent()
    {
        if (isInRange)
        {
            return attackState;
        }
        else if (returnToIdle.canSee == false)
        {
            return returnToIdle;
        }
        else
        {
            return this;
        }
    }
}