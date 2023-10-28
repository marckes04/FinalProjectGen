using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject fireBullet;


    private void Start()
    {

    }

    private void Update()
    {
        ShootBullet();
    }

    void ShootBullet()
    {



        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject bullet = Instantiate(fireBullet, transform.position, Quaternion.identity);
            bullet.GetComponent<FireBullet>().Speed *= transform.localScale.x;

            if (MovementMorlen.right)
            {
                fireBullet.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (!MovementMorlen.right)
            {
                fireBullet.GetComponent<SpriteRenderer>().flipX = true;
            }


        }

    }
}