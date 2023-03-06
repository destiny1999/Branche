using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<CapsuleCollider>().center = new Vector3(-0.0001022096f, 0.004355095f, 0.0003367708f);
        GetComponent<CapsuleCollider>().radius = 0.001141677f;
        GetComponent<CapsuleCollider>().height = 0.009293695f;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
