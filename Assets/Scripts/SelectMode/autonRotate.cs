using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autonRotate : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject target = null;
    float value = 0f;
    void Start()
    {
        target = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        value += Time.deltaTime * 1;
        //print(value);
        target.transform.Rotate(new Vector3(0f, 0f, value),1f);
    }
}
