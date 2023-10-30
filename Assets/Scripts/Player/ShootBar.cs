using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootBar : MonoBehaviour
{
    public Slider shootBar;
    private int maxShoot = 100;
    private int currentShoot;

    public static ShootBar instance;

    private WaitForSeconds regenTick = new WaitForSeconds(.1f);
    private Coroutine regen;



    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentShoot = maxShoot; 
        shootBar.maxValue = maxShoot;
        shootBar.value = maxShoot;
    }

    public void UseShooting(int amount)
    {
        if (currentShoot - amount >= 0)
        {
           
            currentShoot -= amount;
            shootBar.value = currentShoot;


            if(regen != null) 
                StopCoroutine(regen);

          regen =  StartCoroutine(RegenPower());
        }

        else
        {
            PlayerShooting.canShoot = false;
        }
    }

    private IEnumerator RegenPower()
    {
        yield return new WaitForSeconds(2);

        while (currentShoot < maxShoot)
        {
           // PlayerShooting.canShoot = true;
            currentShoot += maxShoot / 100;
            shootBar.value = currentShoot;
            yield return regenTick;
        }

        regen = null;
    }

}
