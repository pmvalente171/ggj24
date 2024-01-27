using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RunnerEnemy : Enemy
{
    private Transform player;
    private Transform firstGoal;
    private Transform currentGoal;
    private bool reachedFirstGoal = false;
    [SerializeField] private float speed = 15f;
    [SerializeField] private float switchGoalDistance = 5f;
    [SerializeField] private float transitionSpeed = 1f;
    private bool isTransitioning = false;
    private float transitionProgress = 0f;
    [SerializeField] private float strafeMagnitude = 5f;
    [SerializeField] private float strafeRotationFactor = 0.5f; 
    [SerializeField] private float bumpForce = 20f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        Transform firstGoalTransform = player.Find("PointInFrontOfPlayer");

        if (firstGoalTransform != null)
        {
            this.firstGoal = firstGoalTransform;
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

        Vector3 forwardDirection = (targetPosition - transform.position).normalized;
        Vector3 rightDirection = new Vector3(forwardDirection.z, 0, -forwardDirection.x); // Perpendicular to forward

        // Sinusoidal side-to-side motion
        float sinValue = Mathf.Sin(Time.time * speed) * strafeMagnitude;
        Vector3 strafeDirection = rightDirection * sinValue;

        // Combine forward movement with side-to-side strafing
        Vector3 combinedDirection = forwardDirection + strafeDirection;
        combinedDirection.Normalize(); // Ensure the combined vector is normalized

        // Move the enemy
        transform.position += combinedDirection * speed * Time.deltaTime;

        // Enhanced LookAt with strafe effect
        Vector3 lookAtDirection = forwardDirection + strafeDirection * strafeRotationFactor;
        transform.LookAt(transform.position + lookAtDirection.normalized);
    }


    void OnCollisionEnter(Collision collision)
    {
        var damageable = collision.collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(1);

            if (collision.rigidbody != null)
            {
                Vector3 pushDirection = collision.transform.position - transform.position;
                pushDirection.y = 0; // Optionally, keep the force horizontal
                pushDirection.Normalize();

                float pushForce = this.bumpForce;
                collision.rigidbody.AddForce(pushDirection * pushForce, ForceMode.Impulse);
                base.Die();
            }
        }
    }

}
