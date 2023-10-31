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
            Debug.Log("Enemy Destroyed");
        }
    }

}
