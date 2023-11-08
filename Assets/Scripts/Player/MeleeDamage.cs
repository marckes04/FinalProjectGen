using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    public LayerMask layer;
    public float radius = 1f;
    public int damage = 30;
    public GameObject damageVisualPrefab; // 3D GameObject representing damage
    public float destroyDelay = 0.025f;



    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layer);

        if (hits.Length > 0)
        {
            EnemyLife enemyLife = hits[0].GetComponent<EnemyLife>();
            if (enemyLife != null)
            {
                enemyLife.TakeDamage(damage);

                // Show 3D damage representation
                ShowDamageVisual(hits[0].transform.position);
                 
                // Deactivate the game object
                gameObject.SetActive(false);
            }
        }
    }

    private void ShowDamageVisual(Vector3 position)
    {
        GameObject damageVisual = Instantiate(damageVisualPrefab, position, Quaternion.identity);

        // You can add animations, movement, and other effects to the damage visual GameObject as needed.

        Destroy(damageVisual, destroyDelay);
    }
}
