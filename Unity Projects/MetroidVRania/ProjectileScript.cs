using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Creates a scriptable object to allow for easier creation of various weapon projectiles
[CreateAssetMenu(fileName = "New Projectile", menuName = "Weapon System/New Projectile")]
public class ProjectileScript : ScriptableObject
{
    #region Weapon Variables
    public string projectileName = string.Empty;
    public string projectileType = string.Empty;
    public int projectileDamage;
    public GameObject projectileModel;
    public float speed;
    public float gravity;
    #endregion
}
