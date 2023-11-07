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

    public string explosionTag = "ExplodedEnemy"; // The tag you want to set on the enemy after the explosion.

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent <Animator>();
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
        StartCoroutine(DeactivateEnemyGameObject());
    }

    IEnumerator DeactivateEnemyGameObject()
    {
        anim.Play("Explossion");

        // Change the tag of the enemy to the specified explosionTag.
        gameObject.tag = explosionTag;

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}