using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnMessageController : MonoBehaviour
{
    public float KeepTime = 2;
    static float keepTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        keepTime = KeepTime;
    }

    // Update is called once per frame
    void Update()
    {
        keepTime -= Time.deltaTime;
        if (keepTime <= 0)
        {
            keepTime = KeepTime;
            this.gameObject.SetActive(false);
        }
    }
}
