using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCancel : MonoBehaviour
{
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            GameManager2.Instance.CallCancelBuild();
        }
    }
}
