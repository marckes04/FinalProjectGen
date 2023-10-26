using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    [SerializeField] private int bulletAngle = -45;
    [SerializeField] private int bulletAngle2 = 45;
    private Rigidbody2D myBullet;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {


            //var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            //bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed * new Vector2(-Mathf.Cos(-bulletAngle * Mathf.Deg2Rad), Mathf.Sin(- bulletAngle * Mathf.Deg2Rad));

            //var bullet2 = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            //bullet2.GetComponent<Rigidbody2D>().velocity = bulletSpeed * new Vector2(Mathf.Cos(bulletAngle2 * Mathf.Deg2Rad), Mathf.Sin(bulletAngle2 * Mathf.Deg2Rad));

            var bullet3 = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet3.GetComponent<Rigidbody2D>().velocity = bulletSpeed * Vector2.right;
        }

    }
}