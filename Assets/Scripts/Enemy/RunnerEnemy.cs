using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RunnerEnemy : Enemy
{
    private Transform player;
    [SerializeField] private Transform firstGoal;
    private Transform currentGoal;
    private bool reachedFirstGoal = false;
    [SerializeField] private float speed = 15f;
    [SerializeField] private float switchGoalDistance = 5f; // Threshold distance to switch goal
    [SerializeField] private float transitionSpeed = 1f;
    private bool isTransitioning = false;
    private float transitionProgress = 0f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        if (firstGoal != null)
        {
            this.currentGoal = this.firstGoal;
        }
        else
        {
            this.currentGoal = player;
        }
    }

    private void Update()
    {
        if (!reachedFirstGoal && firstGoal != null)
        {
            float distanceToFirstGoal = Vector3.Distance(transform.position, firstGoal.position);
            if (distanceToFirstGoal <= switchGoalDistance)
            {
                reachedFirstGoal = true;
                isTransitioning = true;
            }
        }

        if (isTransitioning)
        {
            transitionProgress += Time.deltaTime * transitionSpeed;
            if (transitionProgress >= 1f)
            {
                transitionProgress = 1f;
                isTransitioning = false;
                currentGoal = player;
            }
        }

        MoveTowardsCurrentGoal();
    }

    private void MoveTowardsCurrentGoal()
    {
        Vector3 targetPosition = currentGoal.position;

        if (isTransitioning)
        {
            targetPosition = Vector3.Lerp(firstGoal.position, player.position, transitionProgress);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        transform.LookAt(targetPosition);
    }
}
