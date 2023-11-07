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
        anim = GetComponent<Animator>();
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
        // Disable collisions with game objects tagged as "Player"
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            Physics2D.IgnoreCollision(collider, GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), true);
        }

        anim.Play("Explossion");

        // Change the tag of the enemy to the specified explosionTag.
        gameObject.tag = explosionTag;

        yield return new WaitForSeconds(2f);

        // Re-enable collisions with game objects tagged as "Player"
        foreach (Collider2D collider in colliders)
        {
            Physics2D.IgnoreCollision(collider, GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), false);
        }

        Destroy(gameObject);
    }
}