using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class T_BtController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject RoleInfo = null;
    [SerializeField] TMP_Text TMP_RoleText = null;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClickRole(int which)
    {
        string info = "";
        if(which == 1)
        {
            info = "I can make cake";
        }
        else if(which == 2)
        {
            info = "I can fry steak";
        }
        else if (which == 3)
        {
            info = "I can make drinks";
        }
        else if(which == 4)
        {
            info = "I can make any two meals";
        }
        else if (which == 5)
        {
            info = "I can expand the store";
        }
        else if (which == 6)
        {
            info = "I can welcome guests";
        }
        else if (which == 7)
        {
            info = "I can clear all plates";
        }
        TMP_RoleText.text = info;
        RoleInfo.SetActive(true);
    }
    public void BackToMainView()
    {
        DontDestroyOnLoad(GameObject.Find("UserData"));
        SceneManager.LoadScene("SelectMode");
    }
}
