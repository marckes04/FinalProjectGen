using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    public static EnemyLife Instance;

    [SerializeField] private int maxHealth = 90;
    [SerializeField] private int currentHealth;

    private Animator anim;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int Amount)
    {
        currentHealth -= Amount;
        if (currentHealth <= 0) 
        {
            anim.Play("Explossion");
            DeactivateScript();
        }
    }

    public void DeactivateScript()
    {
        

        StartCoroutine(DeactivateEnemyGameObject());

    }

    IEnumerator DeactivateEnemyGameObject()
    {
        
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

}