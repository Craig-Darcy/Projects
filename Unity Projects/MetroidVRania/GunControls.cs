using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControls : MonoBehaviour
{
    //References needed for the gun to function and initial ammo count
    #region Weapon Data

    public ProjectileScript bulletScript;
    public GameObject bulletObject;
    public WeaponScript gunScript;
    int originalAmmo;
    #endregion

    //Variables for what the gun is like in play (E.g current ammo, if the player if reloading, etc)
    #region Guns Current State

    int bulletsLeft;
    int bulletsShot;
    bool shooting;
    bool weaponsLive;
    bool reloading;

    #endregion


    #region Targetting

    public Camera playerCamera; // Used to point the raycast to where the player wants to fire
    public Transform muzzle; // Used to instanciate the projectile

    #endregion

    public bool allowInvoke = true;

    private void Awake()
    {
        //Sets the gun data before game starts
        bulletsLeft = gunScript.magSize;
        weaponsLive = true;
        originalAmmo = gunScript.weaponAmmo;
        gunScript.weaponAmmo = originalAmmo;
    }
    void Start()
    {
        bulletObject = bulletScript.projectileModel;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    void PlayerInput()
    {
        #region Checks for weapon fire type
        // Allows player to hold down to fire continuously other wise they fire one shot per button press
        if (gunScript.fullyAutomatic == true)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        #endregion

        #region Call Shooting

        if (weaponsLive == true && shooting == true && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;

            GunShooting();
        }

        #endregion

        #region Call Reload
        //Reloads the gun when the button is pressed or automatically when the mag is empty
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < gunScript.magSize && !reloading && gunScript.weaponAmmo > 0)
        {
            Reload();
        }
        if (weaponsLive && shooting && !reloading && bulletsLeft <= 0 && gunScript.weaponAmmo > 0)
        {
            Reload();
        }
        #endregion
    }

    void GunShooting()
    {
        weaponsLive = false;

        #region Raycast

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        #endregion

        #region Raycast Hits

        Vector3 targetArea;

        if (Physics.Raycast(ray, out hit))
        {
            targetArea = hit.point; //Hit point of the raycast
        }
        else
        {
            targetArea = ray.GetPoint(100); //Point 100 units away if the ray doesn't hit anything
        }

        #endregion

        #region Spread Calc
        float spreadX = Random.Range(-gunScript.spreadPattern, gunScript.spreadPattern);
        float spreadY = Random.Range(-gunScript.spreadPattern, gunScript.spreadPattern);
        #endregion

        #region Distance Calc

        Vector3 shotDirectionNoSpread = targetArea - muzzle.position;
        Vector3 shotDirectionWithSpread = shotDirectionNoSpread + new Vector3(spreadX, spreadY, 0);
        #endregion

        #region Instantiate bullet

        GameObject currentBullet = Instantiate(bulletObject, muzzle.position, Quaternion.identity); //Creates bullet object
        currentBullet.transform.forward = shotDirectionWithSpread.normalized; //Sets bullet direction

        //Applies force and gravity to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(shotDirectionWithSpread.normalized * bulletScript.speed, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(playerCamera.transform.up * bulletScript.gravity, ForceMode.Impulse);
        #endregion

        #region Ammo Count

        bulletsLeft--;
        bulletsShot++;

        #endregion

        if (allowInvoke)
        {
            Invoke("ResetShot", gunScript.timeBetweenShooting); //Resets the shot based on the time between shooting
            allowInvoke = false;
        }
        if (bulletsShot < gunScript.burstType && bulletsLeft > 0)
        {
            Invoke("GunShooting", gunScript.timeBetweenShots); //Calls GunShooting for burst type weapons
        }
    }

    void ResetShot()
    {
        weaponsLive = true;
        allowInvoke = true;
    }

    void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", gunScript.timeToReload); //Calls ReloadFinished based on time to reload
    }

    //Refills the mag for the gun and subtracts the size from total ammo
    void ReloadFinished()
    {
        bulletsLeft = gunScript.magSize;
        reloading = false;
        gunScript.weaponAmmo = gunScript.weaponAmmo - gunScript.magSize;
    }
}
