using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class FV_ButtonController : MonoBehaviour
{
    [SerializeField] GameObject SelectCanvas = null;
    [SerializeField] GameObject ScriptController = null;
    [SerializeField] GameObject FirstView = null;
    [SerializeField] GameObject PlayerNameView = null;
    [SerializeField] TMP_Text tmp_playerName = null;
    [SerializeField] GameObject Message = null;

    static bool first = true;
    public static FV_ButtonController Instance;
    private UserData user = null;
    private string userId = "";
    public static bool PostNewUserCompleted = false;

    void Awake()
    {
        Instance = this;
        
        if (!first)
        {
            SelectCanvas.SetActive(true);

            this.gameObject.SetActive(false);
            ScriptController.SetActive(true);
            FirstView.SetActive(false);   
        }
    }

    public void exitGame()
    {
        Application.Quit();
    }
    public void ClickLogin()
    {
        SelectCanvas.SetActive(true);
        this.gameObject.SetActive(false);
        ScriptController.SetActive(true);
        FirstView.SetActive(false);
        first = false;
        
    }
    public void ClickRegister()
    {

    }
    public void ShowSetPlayerNameView(UserData user, string userId)
    {
        this.user = user;
        this.userId = userId;
        GameObject.Find("Email").SetActive(false);
        GameObject.Find("Pswd").SetActive(false);
        GameObject.Find("Login").SetActive(false);
        GameObject.Find("Register").SetActive(false);
        PlayerNameView.SetActive(true);
    }
    public void SetPlayerName()
    {
        HashSet<string> allName = new HashSet<string>();
        if (tmp_playerName.text.Length > 10)
        {
            Message.SetActive(true);
            ObjectDisappear.Instance.setMessage("The name should shorter than 10");
            
            ObjectDisappear.Instance.callDisappear();
            //print("the name should shorter than 10");
        }
        else
        {
            DatabaseHandler.GetAllUserName(allName);
            StartCoroutine(GetName(allName));
        }
        
        //User.Instance.
    }
    IEnumerator GetName(HashSet<string> allName)
    {
        while (allName.Count == 0)
        {
            yield return 0;
        }
        if (!allName.Contains(tmp_playerName.text))
        {
            AuthHandler.Instance.ShowWaiting();
            User.Instance.userData.name = tmp_playerName.text;
            Record[] records = User.Instance.getRecords();
            
            records = new Record[5];
            GameStatusRecords[] gameStatusRecords = User.Instance.getGameStatusRecords();
            gameStatusRecords = new GameStatusRecords[5];
            for (int i = 0; i<records.Length; i++)
            {
                records[i] = new Record();
                gameStatusRecords[i] = new GameStatusRecords();
            }
            //DatabaseHandler.UpdateUserAllData(User.Instance.userData, User.Instance.userData.localId, PostNewUserCompleted);
            DatabaseHandler.UpdateUserAllDataContainRecords(User.Instance.userData, User.Instance.userData.localId, PostNewUserCompleted);
            while (!PostNewUserCompleted)
            {
                yield return 0;
            }
            DatabaseHandler.GetUserData(User.Instance.userData, User.Instance.userData.localId);
            AuthHandler.Instance.HideWaiting();
            PlayerNameView.SetActive(false);
            
        }
        else
        {
            Message.SetActive(true);
            ObjectDisappear.Instance.setMessage("The name has already been used");
            
            ObjectDisappear.Instance.callDisappear();
            //print("The name has been used");
        }
    }
    /*public void clickProfile()
    {
        StartCoroutine(toGetRecords());
    }*/
    /*public IEnumerator toGetRecords()
    {
        /*User user = GameObject.Find("UserData").GetComponent<User>();
        StartCoroutine(DatabaseHandler.GetRecords(user, user.userData.localId));
        loading.SetActive(true);
        Bt_changeView.SetActive(false);
        while (!waitMechanism.getOk())
        {
            yield return 0;
        }
        waitMechanism.SetOkToFalse();
        loading.SetActive(false);
        ProfileView.SetActive(true);
        
    }*/
    
}
