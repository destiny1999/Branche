using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinColor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] Color = null;
    void Start()
    {
        ChangeColor(User.Instance.userData.deskColorIndex);
    }

    public void ChangeColor(int index)
    {
        User.Instance.userData.deskColorIndex = index;
        for(int i = 0; i<Color.Length; i++)
        {
            if (i != index)
            {
                Color[i].transform.gameObject.SetActive(false);
            }
            else
            {
                Color[i].transform.gameObject.SetActive(true);
            }
        }
        
    }
    public void UpdateData()
    {
        DatabaseHandler.Instance.CallUpdateUserAllData(User.Instance.userData);
    }
    
}
