using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject fireBullet;

    private Animator animator;

    public static bool canShoot = true;

    private AnimationAttack magicAttack;

    private void Start()
    {
        animator = GetComponent<Animator>();
        magicAttack = GetComponent<AnimationAttack>();
    }

    private void Update()
    {
        ShootBullet();
    }

    void ShootBullet()
    {

        if (canShoot)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (Random.Range(1, 2) > 0)
                {
                    magicAttack.MagicAttack();
                }


                GameObject bullet = Instantiate(fireBullet, transform.position, Quaternion.identity);
                bullet.GetComponent<FireBullet>().Speed *= transform.localScale.x;


                if (MovementMorlen.right)
                {
                    fireBullet.GetComponent<SpriteRenderer>().flipX = true;
                }
                else if (!MovementMorlen.right)
                {
                    fireBullet.GetComponent<SpriteRenderer>().flipX = false;
                }

                ShootBar.instance.ShootUse();

            }
        }
          
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Farmer"))
        {
            ShootBar.instance.RechargeMagic();
            Destroy(other.gameObject);
            canShoot = true;
        }
    }

}