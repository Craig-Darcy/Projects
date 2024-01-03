using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to fire the guns in the game by instanciating the projectile based off the position of the gun barrel 
public class GunShootingScript : MonoBehaviour
{
    public float bulletSpeed;
    public ProjectileScript bulletScript;
    GameObject bullet;
    public Transform barrel;

    private void Start()
    {
        bullet = bulletScript.projectileModel;
        bulletSpeed = bulletScript.speed;
    }
    public void Fire()
    {
        GameObject spawnBullet = Instantiate(bullet, barrel.position, barrel.rotation);
        spawnBullet.GetComponent<Rigidbody>().velocity = bulletSpeed * barrel.forward;
    }
}
