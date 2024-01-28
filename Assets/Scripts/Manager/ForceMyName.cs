using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceMyName : MonoBehaviour
{
    public string myName = "";

    void Awake()
    {
        if (myName != "")
        {
            this.gameObject.name = myName;
        }
    }

}
