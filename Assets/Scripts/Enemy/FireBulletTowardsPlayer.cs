using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBulletTowardsPlayer : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;


    void Update(){
        if (Input.GetKeyDown(KeyCode.LeftControl)){
            Fire();
        }
    }

    public void Fire()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector3 direction = player.transform.position - this.transform.position;
            direction.Normalize();

            GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
        }
    }
}
    

