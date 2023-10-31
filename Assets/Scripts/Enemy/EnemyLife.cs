using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField]private float Life = 90;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))

        {
            Life -= 30;
        }
    
    }



}
