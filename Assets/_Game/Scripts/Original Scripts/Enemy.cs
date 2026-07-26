using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script defines 'Enemy's' health and behavior. 
/// </summary>
public class Enemy : MonoBehaviour {

    [SerializeField] GameObject shieldVisual;

    #region FIELDS
    [Tooltip("Health points in integer")]
    public int health;

    [Tooltip("Enemy's projectile prefab")]
    public Projectile Projectile;

    [Tooltip("VFX prefab generating after destruction")]
    public GameObject destructionVFX;
    public GameObject hitEffect;

    [HideInInspector] public int shotChance; //probability of 'Enemy's' shooting during tha path
    [HideInInspector] public float shotTimeMin, shotTimeMax; //max and min time for shooting from the beginning of the path
    [HideInInspector] public int shieldChance = -1;
    public bool IsShielded { get; private set; }
    #endregion

    private void Start()
    {
        shieldVisual.SetActive(false);
        LevelController.OnEnemyCountChanged?.Invoke(1);
        if(Random.value < (float)shieldChance / 100)
        {
            ActivateShield();
        }

        Invoke("ActivateShooting", Random.Range(shotTimeMin, shotTimeMax));
    }

    //coroutine making a shot
    void ActivateShooting() 
    {
        if (Random.value < (float)shotChance / 100 && Projectile)                             //if random value less than shot probability, making a shot
        {                         
            Instantiate(Projectile,  gameObject.transform.position, Quaternion.identity);             
        }
    }

    public void ActivateShield()
    {
        IsShielded = true;

        if (shieldVisual)
        {
            shieldVisual.SetActive(IsShielded);
        }
    }

    //method of getting damage for the 'Enemy'
    public void GetDamage(int damage) 
    {
        if (IsShielded)
        {
            if(shieldVisual)
                shieldVisual.SetActive(false);
            IsShielded = false;
            return;
        }

        health -= damage;           //reducing health for damage value, if health is less than 0, starting destruction procedure
        if (health <= 0)
            Destruction();
        else
        {
            if(hitEffect)
                Instantiate(hitEffect, transform.position, Quaternion.identity, transform);
        }
           
    }    

    //if 'Enemy' collides 'Player', 'Player' gets the damage equal to projectile's damage value
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Projectile != null)
                Player.instance.GetDamage(Projectile.damage);
            else
                Player.instance.GetDamage(1);
        }
    }

    //method of destroying the 'Enemy'
    void Destruction()                           
    {
        if (destructionVFX)
            Instantiate(destructionVFX, transform.position, Quaternion.identity);
        LevelController.OnEnemyCountChanged?.Invoke(-1);
        Destroy(gameObject);
    }


}
