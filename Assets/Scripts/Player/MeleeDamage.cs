using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    public LayerMask layer;
    public float radius = 1f;
    public int damage = 30;

    void Update()
    {

        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layer);

        if (hits.Length > 0)
        {

            hits[0].GetComponent<EnemyLife>().TakeDamage(damage);

            gameObject.SetActive(false);

        }

    }
}
