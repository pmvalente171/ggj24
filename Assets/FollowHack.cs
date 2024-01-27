using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHack : MonoBehaviour
{
    [SerializeField] private Transform target;
    Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    private void Update()
    {
        transform.position = target.position + offset;
        transform.rotation = target.rotation;
    }
}
