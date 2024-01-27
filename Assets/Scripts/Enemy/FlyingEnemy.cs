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
    public float rotationSpeed = 2f; // Speed at which the enemy rotates towards the player

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Set the initial rotation to look at the player
        Vector3 directionToPlayer = player.position - transform.position;
        Quaternion initialRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = initialRotation;
    }

    private void Update()
    {
        Hover();
        MaintainDistance();
        RotateTowardsPlayer();
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
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // Keep the rotation only on the Y axis

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
