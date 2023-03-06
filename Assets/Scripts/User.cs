using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class User : MonoBehaviour
{
    [SerializeField] TMP_Text Level = null;
    [SerializeField] TMP_Text Name = null;
    [SerializeField] TMP_Text WinRate = null;
    [SerializeField] TMP_Text Money = null;
    public UserData userData = null;
    [SerializeField] Record[] records = new Record[5];
    [SerializeField] GameStatusRecords[] gameStatusRecords = new GameStatusRecords[5];
    //[SerializeField]
    public static User Instance;
    public static bool login = false;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        if (login)
        {
            
            //GameObject.Destroy(GameObject.Find("UserData"));
            SetData(GameObject.Find("UserData").GetComponent<User>().userData);
            setRecords(GameObject.Find("UserData").GetComponent<User>().getRecords());
            setGameStatusRecords(GameObject.Find("UserData").GetComponent<User>().getGameStatusRecords());
            Destroy(GameObject.Find("UserData"));
        }
    }
    public void SetData(UserData data)
    {
        userData = new UserData(data);
        
        Level.text = "Lv. " + data.lv;
        Name.text = data.name;
        var winRate = data.allGame > 0 ? data.winningGame / data.allGame : 0;
        WinRate.text = winRate + "%";
        Money.text = "M:" + data.money;
        
    }
    public Record[] getRecords()
    {
        return records;
    }
    public void setRecords(Record[] _records)
    {
        records = _records;
    }
    public GameStatusRecords[] getGameStatusRecords()
    {
        return gameStatusRecords;
    }
    public void setGameStatusRecords(GameStatusRecords[] _gameStatusRecords)
    {
        gameStatusRecords = _gameStatusRecords;
    }
    public void setFinalRecords(Record[] _records)
    {
        records = _records;
    }
}
