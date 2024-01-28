using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    public Transform player;
    public float baseHeight = 5f;
    public float hoverAmplitude = 0.5f; 
    public float hoverFrequency = 1f; 
    public float desiredDistance = 10f; 
    public float rotationSpeed = 2f;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Update()
    {
        Hover();
        MaintainDistance();
        RotateTowardsPlayer(); // Call this method to align at the start
    }

    private void Hover()
    {
        float newY = baseHeight + Mathf.Sin(Time.time * hoverFrequency) * hoverAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void MaintainDistance()
    {
        Vector3 directionToPlayer = transform.position - player.position;
        float distance = directionToPlayer.magnitude;

        if (distance < desiredDistance)
        {
            Vector3 newPos = transform.position + (directionToPlayer.normalized * (desiredDistance - distance));
            transform.position = new Vector3(newPos.x, transform.position.y, newPos.z);
        }
    }

    private void RotateTowardsPlayer()
    {
        // Calculate the horizontal direction to the player
        Vector3 direction = new Vector3(
            player.position.x - transform.position.x,
            0f, // Zero out the y component
            player.position.z - transform.position.z
        );

        // Create a rotation based on the horizontal direction
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Rotate the enemy
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

}
