using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AdjustItemController : MonoBehaviour
{
    [SerializeField] TMP_Text TMP_value = null;
    [SerializeField] float parameterType = 0;
    public static AdjustItemController Instance = null;
    float value = 1;
    int max = 0;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        max = parameterType == 1 ? 4 : parameterType == 2 ? 3 : 3;
    }
    public void addValue()
    {
        if (value < max)
        {
            value++;
            if(parameterType == 1)
            {
                OperatingController.Instance.addGuest(value);
            }
            else if(parameterType == 2)
            {
                OperatingController.Instance.addPeople(value);
            }
            else
            {
                OperatingController.Instance.addItem(value);
            }
        }
        TMP_value.text = value + "";
    }
    public void subValue()
    {
        if (value > 1)
        {
            value--;
            if(parameterType == 1)
            {
                OperatingController.Instance.subGuest(value);
            }
            else if (parameterType == 2)
            {
                OperatingController.Instance.subPeople(value);
            }
            else
            {
                OperatingController.Instance.subItem(value);
            }
        }
        TMP_value.text = value + "";
    }
    // Update is called once per frame
    
}
