using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tips : MonoBehaviour
{
    // Start is called before the first frame update
    static bool showTips = true;
    void Start()
    {
        showTips = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TipClick()
    {
        if (showTips)
        {
            GameManager2.Instance.CallCheckStatus();
            showTips = false;
        }
        else
        {
            GameManager2.Instance.CallClearTips();
            showTips = true;
        }
    }
}
