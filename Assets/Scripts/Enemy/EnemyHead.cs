using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    private Enemy parentComponent;

    void Start()
    {
        this.parentComponent = GetComponentInParent<Enemy>();
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {   
            Debug.Log("HEADHSOT!!!");
            this.parentComponent.TakeDamage(1000);
        }
    }
}
    
