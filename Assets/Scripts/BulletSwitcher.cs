using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSwitcher : MonoBehaviour
{
    public Bullet bullet;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
        {
            other.gameObject.GetComponent<Character>().bullet = bullet.gameObject;
            other.gameObject.GetComponent<Character>().newGun = bullet.gun;
        }
    }
}
