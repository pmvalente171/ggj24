using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerEnemy : Enemy
{
    private Transform player;
    private Transform firstGoal;
    private bool reachedFirstGoal = false;
    [SerializeField] private float speed = 15f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        transform.LookAt(player);
    }
}
    
