using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//guns objects in 'Player's' hierarchy
[System.Serializable]
public class Guns
{
    public GameObject rightGun, leftGun, centralGun;
    [HideInInspector] public ParticleSystem leftGunVFX, rightGunVFX, centralGunVFX; 
}

public class PlayerShooting : MonoBehaviour {

    [Tooltip("shooting frequency. the higher the more frequent")]
    public float fireRate;

    [Tooltip("projectile prefab")]
    public GameObject projectileObject;

    //time for a new shot
    [HideInInspector] public float nextFire;


    [Tooltip("current weapon power")]
    [Range(1, 4)]       //change it if you wish
    public int weaponPower = 1; 

    public Guns guns;
    public bool shootingIsActive = true; 
    [HideInInspector] public int maxweaponPower = 4; 
    public static PlayerShooting instance;

    private void Awake()
    {
        if (instance == null)
        {
            Debug.Log("Player Awake");
            instance = this;
        }
            
    }
    private void Start()
    {
        if(guns == null) { return; }

        if(guns.leftGun)
            guns.leftGunVFX = guns.leftGun.GetComponent<ParticleSystem>();
        //receiving shooting visual effects components
        if (guns.rightGun)
            guns.rightGunVFX = guns.rightGun.GetComponent<ParticleSystem>();

        if (guns.centralGun)
            guns.centralGunVFX = guns.centralGun.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (shootingIsActive)
        {
            if (Time.time > nextFire)
            {
                MakeAShot();                                                         
                nextFire = Time.time + 1 / fireRate;
            }
        }
    }

    //method for a shot
    void MakeAShot() 
    {
        if (guns == null) return;

        switch (weaponPower) // according to weapon power 'pooling' the defined anount of projectiles, on the defined position, in the defined rotation
        {
            case 1:
                if(projectileObject)
                    CreateLazerShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
                if (guns.centralGunVFX)
                    guns.centralGunVFX.Play();
                break;

            case 2:
                if (projectileObject)
                {
                    CreateLazerShot(projectileObject, guns.rightGun.transform.position, Vector3.zero);
                    CreateLazerShot(projectileObject, guns.leftGun.transform.position, Vector3.zero);
                }
                if (guns.leftGunVFX)
                    guns.leftGunVFX.Play();
                if (guns.rightGunVFX)
                    guns.rightGunVFX.Play();
                break;

            case 3:
                if (projectileObject)
                {
                    CreateLazerShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
                    CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -5));
                    CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 5));
                }

                if (guns.leftGunVFX)
                    guns.leftGunVFX.Play();

                if (guns.rightGunVFX)
                    guns.rightGunVFX.Play();
                break;

            case 4:
                if (projectileObject)
                {
                    CreateLazerShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
                    CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -5));
                    CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 5));
                    CreateLazerShot(projectileObject, guns.leftGun.transform.position, new Vector3(0, 0, 15));
                    CreateLazerShot(projectileObject, guns.rightGun.transform.position, new Vector3(0, 0, -15));
                }

                if (guns.leftGunVFX)
                    guns.leftGunVFX.Play();

                if (guns.rightGunVFX)
                    guns.rightGunVFX.Play();

                break;
        }
    }

    void CreateLazerShot(GameObject lazer, Vector3 pos, Vector3 rot) //translating 'pooled' lazer shot to the defined position in the defined rotation
    {
        Instantiate(lazer, pos, Quaternion.Euler(rot));
    }
}
