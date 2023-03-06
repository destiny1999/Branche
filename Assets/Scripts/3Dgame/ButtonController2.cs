using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ButtonController2 : MonoBehaviour
{
    
    [SerializeField] GameObject ChangeCharacter = null;
    [SerializeField] GameObject PublicCharacter = null;
    [SerializeField] GameObject SupportCharacter = null;
    [SerializeField] GameObject Call = null;
    [SerializeField] GameObject Support = null;
    [SerializeField] GameObject BuilderAbilityView = null;
    [SerializeField] GameObject UsePlayer = null;
    [SerializeField] GameObject EndTurn = null;
    [SerializeField] GameObject TipsContent = null;

    
    //[SerializeField] GameObject 

    public static ButtonController2 Instance;
  
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClickTips()
    {
        TipsContent.SetActive(!TipsContent.activeSelf);
    }

    public void ClickCall()
    {
        ChangeCharacter.SetActive(true);
        PublicCharacter.SetActive(true);
    }
    public void ClickChange(int which)
    {
        Call.SetActive(false);
        PublicCharacter.SetActive(false);
        ChangeCharacter.SetActive(false);
        Support.SetActive(true);
        UsePlayer.SetActive(true);
        GameManager2.Instance.ChangeCharacter(which);
    }
    public void ClickSupport()
    {
        PublicCharacter.SetActive(true);
        SupportCharacter.SetActive(true);
    }
    public void ClickSupportUse(int which)
    {
        PublicCharacter.SetActive(false);
        SupportCharacter.SetActive(false);
        Support.SetActive(false);
        GameManager2.Instance.SupportUse(which, false);
        //ActionController.Instance.addAction(2);// use other
    }
    public void ClickBtLevelUpBonuse(int code)
    {
        //print("Click Bonuse level up ");
        GameManager2.Instance.BuildClick(code);
        BuilderAbilityView.SetActive(false);
    }
    public void ClickBtLevelUpTable(int code)
    {
        //print("Click table level up ");
        GameManager2.Instance.BuildClick(code);
        BuilderAbilityView.SetActive(false);
    }
    public void ShowBuildAbilityView()
    {
        BuilderAbilityView.SetActive(true);
        //BuilderAbilityView.GetComponent<BuildViewSetting>().enabled = true;
    }
    public void HideBuildAbilityView()
    {
        BuilderAbilityView.SetActive(false);
        //BuilderAbilityView.GetComponent<BuildViewSetting>().enabled = true;
    }
    public void ClickUsePlayer(int who)// 1 player1 2 player2 ...
    {
        GameManager2.Instance.UsePlayerClick(who);
    }
    
    public void ClickEndTurn()
    {
        if (GameManager2.Instance.getChangeAction())
        {
            if (BuilderAbilityView.activeSelf)
            {
                BuilderAbilityView.SetActive(false);
            }
            EndTurn.SetActive(false);
            GameManager2.Instance.TurnEnd();
        }
        else
        {
            Debug.Log("Didn't changeRole");
        }
    }
    public void ExitGame()
    {
        UserData user = GameObject.Find("UserData").GetComponent<User>().userData;
        StartCoroutine(GameMethods2.Instance.SaveResultData(user));
        DontDestroyOnLoad(GameObject.Find("UserData"));
        SceneManager.LoadScene("Lobby");
    }
}
