using UnityEngine;

public class Bonus : MonoBehaviour {

    //when colliding with another object, if another objct is 'Player', sending command to the 'Player'

    public void PickUp()
    {
        if (PlayerShooting.instance.weaponPower < PlayerShooting.instance.maxweaponPower)
        {
            PlayerShooting.instance.weaponPower++;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.tag == "Player") 
        {
            PickUp();
        }
    }
}
