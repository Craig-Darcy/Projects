using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidBallBehaviour : MonoBehaviour
{
    #region Projectile Variables
    public ProjectileScript AcidStats;
    public GameObject AcidSplashParticles;
    public float explodeDelay = 2f;
    float explodeTimer;
    bool hasExploded = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Sets the initial explosion timer for the projectile
        explodeTimer = explodeDelay;
    }

    // Update is called once per frame
    void Update()
    {
        explodeTimer -= Time.deltaTime;
        if(explodeTimer <= 0 && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    //Checks if the acid ball has collider with another object
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            explodeTimer = 0;
        }
        if (other.gameObject.tag == "Grate")
        {
            explodeTimer = 0;
        }
    }

    //Handles acid ball explosion
    void Explode()
    {
        //Instantiates the partile effects of the explosion
        GameObject particles = Instantiate(AcidSplashParticles, transform.position, transform.rotation);

        //Destroys the particle effects after 4 seconds
        Destroy(particles, 4.0f);
        
        //Destroys the projectile game object (Prevents an infinte loop or explosions and particle effects)
        Destroy(this.gameObject, 0.1f);
    }

}
