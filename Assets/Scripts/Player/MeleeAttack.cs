using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private AnimationAttack playerAnimation;

    public GameObject attack;



     void Awake()
    {
      playerAnimation = GetComponent<AnimationAttack>();       
    }




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            if (Random.Range(0, 2) > 0)
            {
                playerAnimation.AttackMelee();
            }

        }
    }


    public void Damage()
    {
        attack.gameObject.SetActive(true);
    }

}
