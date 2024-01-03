using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates a scriptable object that allows for easier creation of guns for the game
[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon System/New Weapon")]
public class WeaponScript : ScriptableObject
{
    #region Weapon Variables
    public string weaponName = string.Empty;
    public string weaponType = string.Empty;
    public int weaponAmmo;
    public int magSize;
    public GameObject weaponModel;
    public Sprite weaponSprite;
    public bool weaponToggled;
    public bool fullyAutomatic;
    #endregion

    #region Weapon Behvaiour
    public float timeBetweenShooting;
    public float spreadPattern;
    public float timeToReload;
    public float timeBetweenShots;
    public int burstType;
    #endregion
}