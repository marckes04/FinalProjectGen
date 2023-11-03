using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    public static EnemyLife Instance;

    [SerializeField] private int maxHealth = 90;
    [SerializeField] private int currentHealth;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int Amount)
    {
        currentHealth -= Amount;
        if (currentHealth <= 0) 
        {
           DeactivateScript();
        }
    }

    public void DeactivateScript()
    {

       // enemyDied = true;

        StartCoroutine(DeactivateEnemyGameObject());
        Destroy(gameObject);

    }

    IEnumerator DeactivateEnemyGameObject()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

}
