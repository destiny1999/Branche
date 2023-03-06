using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class UserData
{
    public string name="";
    public int money = 0;

    public int lv = 1;
    public int allGame = 0;
    public int winningGame = 0;
    public int leftGame = 0;
    public string localId="";
    public int deskColorIndex = 0;
    public int deskSkin = 0;

    public string idToken="";
    public UserData userData;

    public UserData()
    {

    }

    public UserData(UserData data)
    {
        Debug.Log("ID = " + data.localId);
        userData = data;
        name = data.name;
        money = data.money;
        lv = data.lv;
        allGame = data.allGame;
        leftGame = data.leftGame;
        localId = data.localId;
        deskColorIndex = data.deskColorIndex;
        idToken = data.idToken;
    }
    
}


