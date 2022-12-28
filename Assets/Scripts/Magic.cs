using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{

    public Transform el;
    public GameObject energyBolt;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(energyBolt, el.position, el.rotation);
        }
    }
}
