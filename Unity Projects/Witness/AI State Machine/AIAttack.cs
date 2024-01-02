using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Derived class of State, this is the logic for the attack state
public class AIAttack : State
{
    //Other states in the state machine
    public AIChase returnToChase;

    //Enemy Variables
    public float attackSpeed;
    public bool hasAttacked;
    private Vector3 target;

    //Player Variables
    public StatblockScript healthStats;
    public StatblockScript sanityStats;
    private Transform player;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;//Finds players location when this state is called

        target = new Vector3(player.position.x, player.position.y, player.position.z);//targets that location for attack
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, attackSpeed * Time.deltaTime);//Move to the player during the attack
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasAttacked = true;
            if (returnToChase.isInRange)
            {
                healthStats.playerHealth = healthStats.playerHealth - 1;
                sanityStats.playerSanity = sanityStats.playerSanity - 1;
            }
        }
    }
    public override State RunCurrent()
    {
        if (returnToChase.isInRange == false)
        {
            return returnToChase;
        }
        else
        {
            return this;
        }
    }
}
