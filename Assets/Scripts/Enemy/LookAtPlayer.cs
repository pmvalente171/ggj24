using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private GameObject player;
    [SerializeField] float rotationSpeed = 2f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player != null)
        {
            // Calculate the horizontal direction to the player
            Vector3 direction = new Vector3(
                player.transform.position.x - transform.position.x,
                0f, // Keep y-component zero to maintain current height
                player.transform.position.z - transform.position.z
            ).normalized;

            // Calculate the angle to rotate on the y-axis
            float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg - 90f;

            // Rotate only around the y-axis
            transform.rotation = Quaternion.Euler(0f, -angle, 0f);
        }
    }
}
