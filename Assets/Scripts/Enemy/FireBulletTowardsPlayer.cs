using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBulletTowardsPlayer : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float fireRate = 5.1f;
    [SerializeField] private float offScreenFireRate = 1.5f;
    [SerializeField] private bool shouldFire = true;
    [SerializeField] private Transform firePoint;


    void Start()
    {
        if (this.firePoint == null){
            this.firePoint = this.transform;
        }
        StartCoroutine(FireRoutine());
    }

    private IEnumerator FireRoutine()
    {
        while (shouldFire)
        {
            Camera mainCamera = Camera.main;
            Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);
            bool isInView = viewportPosition.z > 0 && viewportPosition.x > 0 && viewportPosition.x < 1 && viewportPosition.y > 0 && viewportPosition.y < 1;

            yield return new WaitForSeconds(fireRate);
            if (!isInView)
            {
                yield return new WaitForSeconds(offScreenFireRate);
            }
            Fire();
        }
    }

    public void Fire()
    {
        Camera mainCamera = Camera.main;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector3 direction = player.transform.position - this.firePoint.position;
            direction.Normalize();

            GameObject bullet = Instantiate(bulletPrefab, this.firePoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
        }
    }

}
    

