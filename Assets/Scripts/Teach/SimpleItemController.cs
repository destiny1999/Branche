using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleItemController : MonoBehaviour
{
    [SerializeField] float itemValue = -1;
    [SerializeField] Sprite cake = null;
    [SerializeField] Sprite juice = null;
    [SerializeField] Sprite steak = null;
    [SerializeField] Image showItem = null;

    // Start is called before the first frame update
    public void SetUp()
    {
        float value = OperatingController.Instance.getItemSelection();
        bool action = OperatingController.Instance.getAction();
        if (!action)
        {
            switch (value)
            {
                case 1:
                    showItem.sprite = steak;
                    break;
                case 2:
                    showItem.sprite = cake;
                    break;
                case 3:
                    showItem.sprite = juice;
                    break;
            }
            itemValue = value;
        }
        
    }
    public float GetValue()
    {
        return itemValue;
    }
    public void SetValue(float value)
    {
        itemValue = value;
    }
    public void ClickChef()
    {
        OperatingController.Instance.ChefClick(this.gameObject);
    }
}
