using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject fireBullet;

    private Animator animator; 


    private void Start()
    {

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ShootBullet();
    }

    void ShootBullet()
    {


        if (Input.GetKeyDown(KeyCode.Mouse0)) //-change
        {
            GameObject bullet = Instantiate(fireBullet, transform.position, Quaternion.identity);
            bullet.GetComponent<FireBullet>().Speed *= transform.localScale.x;
            animator.SetBool("Melee", true); //-new

            if (MovementMorlen.right)
            {
                fireBullet.GetComponent<SpriteRenderer>().flipX = false;
                animator.GetComponent<SpriteRenderer>().flipX = false; //-new
            }
            else if (!MovementMorlen.right)
            {
                fireBullet.GetComponent<SpriteRenderer>().flipX = true;
                animator.GetComponent<SpriteRenderer>().flipX = true; //-new
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            animator.SetBool("Melee", false);
            if (Input.GetKeyDown(KeyCode.J))
            {
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


            }


        }
    }
}