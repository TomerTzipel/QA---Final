using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    #region FIELDS
    [Tooltip("Health points in integer")]
    public int health;

    [Tooltip("Enemy's projectile prefab")]
    public Projectile Projectile;

    [Tooltip("VFX prefab generating after destruction")]
    public GameObject destructionVFX;
    public GameObject hitEffect;
    public bool canAttack;
    public float shotIntervalMin, shotIntervalMax; //max and min time for shooting from the beginning of the path

    public Transform[] projectileSpawnPositions;
    #endregion

    private void Start()
    {
        LevelController.OnEnemyCountChanged?.Invoke(1);
        StartCoroutine(FireCoroutine());
    }

    private IEnumerator FireCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(shotIntervalMin, shotIntervalMax));
        Shoot();
        StartCoroutine(FireCoroutine());
    }

    void Shoot()
    {
        if (!canAttack) { return; }
        Vector3 position = projectileSpawnPositions[Random.Range(0, projectileSpawnPositions.Length)].position;
        if(Projectile)
            Instantiate(Projectile, position, Quaternion.identity);
    }

    //method of getting damage for the 'Enemy'
    public void GetDamage(int damage)
    {
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
