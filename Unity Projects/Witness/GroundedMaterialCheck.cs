using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedMaterialCheck : MonoBehaviour
{
    //Player Variables
    public Collider playerCollider;
    public bool playerGrounded;

    //Checks for terrain type
    public bool isOnTerrain;
    public bool isOnWoodFloor;
    public bool isOnMetalFloor;
    public bool isOnPavement;
    public bool isOnCarpet;

    RaycastHit hit;

    
    void Update()
    {
        playerGrounded = GroundedPlayer();
        TerrainCheck();
        PavementCheck();
        MetalFloorCheck();
        WoodenFloorCheck();
    }

    bool GroundedPlayer()
    {
        return Physics.Raycast(transform.position, Vector3.down, out hit, playerCollider.bounds.extents.y + 0.5f); //Casts raycast down to ensure the player is grounded
    }

    //These methods then use that same raycast to check what terrain this player is using tags for the different materials on the objects
    public void PavementCheck()
    {
        if (hit.collider != null && hit.collider.tag == "Pavement")
        {
            isOnPavement = true;
        }
        else
        {
            isOnPavement = false;
        }
    }
    public void TerrainCheck()
    {
        if (hit.collider != null && hit.collider.tag == "Terrain")
        {
            isOnTerrain = true;
        }
        else
        {
            isOnTerrain = false;
        }
    }

    public void WoodenFloorCheck()
    {
        if (hit.collider != null && hit.collider.tag == "Wood")
        {
            isOnWoodFloor = true;
        }
        else
        {
            isOnWoodFloor = false;
        }
    }

    public void MetalFloorCheck()
    {
        if (hit.collider != null && hit.collider.tag == "Metal")
        {
            isOnMetalFloor = true;
        }
        else
        {
            isOnMetalFloor = false;
        }
    }
}
