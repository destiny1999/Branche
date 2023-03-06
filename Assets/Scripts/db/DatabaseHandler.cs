using Proyecto26;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DatabaseHandler : MonoBehaviour
{
    private const string projectId = "branchetest";
    private static readonly string databaseURL = $"https://branchetest.firebaseio.com/";
    private const string apiKey = "AIzaSyDJmXQuoqlZWAIVPCUXviEhLALD8_Tj4VQ";
                                    
    UserData user;
    public static DatabaseHandler Instance;

    void Awake()
    {
        Instance = this;
    }

    public static void PostUser(UserData user, string userId)
    {
        //print("Here will post");
        RestClient.Put($"{databaseURL}users/{userId}/data.json", user).Then(response=>
        {
            Debug.Log("Post success");
        }).Catch(Debug.LogException);
       
    }
    public delegate void GetUserCallback(UserData user);
    
    public static void GetUserData(UserData user, string userId)// when login
    {
        //bool esist = false;
        //print("into get user data");
        //print("Test token = " + AuthHandler.idToken);
        RestClient.Get($"{databaseURL}users/{userId}/data.json").Then(respons =>
        {
            var responseJson = respons.Text;
            var data = JsonUtility.FromJson<UserData>(responseJson);
            user = data;
            user.localId = userId;
            Debug.Log("json data = " + responseJson);
            User.Instance.SetData(user);
            if (data.name.Length == 0)
            {
                //print("need name");
                FV_ButtonController.Instance.ShowSetPlayerNameView(user, userId);
            }
            else
            {
                Debug.Log("ChangeView");
                FV_ButtonController.Instance.ClickLogin();
                UpdateUserAllData(user, userId);
            }
            AuthHandler.Instance.HideWaiting();
            //esist = true;
        }).Catch(Debug.LogWarning);


    }
    public static void UpdateUserAllData(UserData user, string userId)
    {
        RestClient.Put($"{databaseURL}users/{userId}/data.json", user).Then(response =>
        {
            Debug.Log("Update data success");
        });
    }
    
    public static void UpdateUserAllData(UserData user, string userId, bool completed)
    {
        RestClient.Put($"{databaseURL}users/{userId}/data.json", user).Then(response =>
        {
            Debug.Log("Update data success");
            waitMechanism.SetOkToTrue();
            //FV_ButtonController.PostNewUserCompleted = true;
        });
    }

    public static void UpdateUserAllDataContainRecords(UserData user, string userId, bool completed)
    {
        Record[] record = new Record[5];
        GameStatusRecords[] gameStatusRecords = new GameStatusRecords[5];
        var str_json = "";
        var str_json2 = "";
        for (int i = 0; i < record.Length; i++)
        {
            record[i] = new Record();
            gameStatusRecords[i] = new GameStatusRecords();
            str_json += JsonUtility.ToJson(record).ToString();
            str_json2 += JsonUtility.ToJson(gameStatusRecords).ToString();
        }
        var data = JsonHelper.ArrayToJsonString(record);
        var data2 = JsonHelper.ArrayToJsonString(gameStatusRecords).ToString();

        RestClient.Put($"{databaseURL}users/{userId}/data.json", user).Then(response =>
        {
            Debug.Log("Update data success");
            RestClient.Put($"{databaseURL}users/{userId}/records.json", data).Then(responseRecords =>
            {
                print("success records");
                //FV_ButtonController.PostNewUserCompleted = true;
            });
        });
        RestClient.Put($"{databaseURL}users/{userId}/gameStatusRecords.json", data2).Then(responseRecords =>
        {
            //print("success update final records");
            FV_ButtonController.PostNewUserCompleted = true;
            //waitMechanism.SetOkToTrue();
        });
    }

    public static void GetRecords(User user, string userId)
    {
        Record[] records = user.getRecords();
        RestClient.Get($"{databaseURL}users/{userId}/records.json").Then(respons =>
        {
            var responseJson = respons.Text;
            records = JsonHelper.FromJsonString<Record>(responseJson);
            User.Instance.setRecords(records);
            print("get records OK");
            waitMechanism.SetOkToTrue();
        }).Catch(Debug.LogWarning);

        
    }
    public static void UpdateRecord()
    {
        Record[] record = User.Instance.getRecords();
        var str_json = "";
        for (int i = 0; i < record.Length; i++)
        {
            
            str_json += JsonUtility.ToJson(record[i]).ToString();
        }
        var data = JsonHelper.ArrayToJsonString(record);

        string userId = User.Instance.userData.localId;

        RestClient.Put($"{databaseURL}users/{userId}/records.json", data).Then(responseRecords =>
        {
            print("success update final records");
            FV_ButtonController.PostNewUserCompleted = true;
            waitMechanism.SetOkToTrue();
        });
    }

    public static void GetData(UserData user, string userId, string target)
    {
        Debug.Log($"GetData target = {target}");
        RestClient.Get($"{databaseURL}users/{userId}/data/{target}.json").Then(respons =>
        {
            Debug.Log($"Try to Get {target}");
            var responseJson = respons.Text;
            Debug.Log(responseJson);
            
            Debug.Log($"Get {target} = " + responseJson);
        });
    }
    public void CallUpdateUserAllData(UserData user)
    {
        UpdateUserAllData(user, user.localId);
    }
    public static void GetAllUserName(HashSet<string> allName)
    {
        
        RestClient.Get($"{databaseURL}users/.json").Then(respons =>
        {
            var responseJson = respons.Text;
            for(int i = 0; i < JSON.Parse(respons.Text).Count; i++)
            {
                var data = JSON.Parse(respons.Text)[i]["data"]["name"];
                allName.Add(data);
            }
        });
        
    }
    public static void GetAllUserId(HashSet<string> allUser)
    {
        RestClient.Get($"{databaseURL}users/.json").Then(respons =>
        {
            var responseJson = respons.Text;
            for (int i = 0; i < JSON.Parse(respons.Text).Count; i++)
            {
                var data = JSON.Parse(respons.Text)[i];
                allUser.Add(data);
            }
        });
    }
    public static void GetGameStatusRecords(User user, string userId)
    {
        GameStatusRecords[] gameStatusRecords = user.getGameStatusRecords();
        RestClient.Get($"{databaseURL}users/{userId}/gameStatusRecords.json").Then(respons =>
        {
            var responseJson = respons.Text;
            gameStatusRecords = JsonHelper.FromJsonString<GameStatusRecords>(responseJson);
            User.Instance.setGameStatusRecords(gameStatusRecords);
            print("get game statusRecords OK");
            waitMechanism.SetOkToTrue();
        }).Catch(Debug.LogWarning);
    }
    /*public static void GetGameStatusRecords(string userId, GameStatusRecords[] gameStatusRecords)
    {
        GameStatusRecords[] allGameStatusRecords = new GameStatusRecords[5];
        RestClient.Get($"{databaseURL}users/{userId}/gameStatusRecords.json").Then(respons =>
        {
            var responseJson = respons.Text;
            allGameStatusRecords = JsonHelper.FromJsonString<GameStatusRecords>(responseJson);

            //print("len = " + allGameStatusRecords.Length);
            for(int i = 0; i<5; i++)
            {
                print(allGameStatusRecords[i].title);
            }
            gameStatusRecords = allGameStatusRecords;
            /*for(int i = 0; i<gameStatusRecords.Length; i++)
            {
                print("inside title = " + gameStatusRecords[i].title);
            }*//*
            Test.SetGameStatusRecords(allGameStatusRecords);
            waitMechanism.SetOkToTrue();
        });
    }*/
    public static void UpdateGameStatusRecords(GameStatusRecords[] gsr, string userId)
    {
        RestClient.Put($"{databaseURL}users/{userId}/gameStatusRecords.json", gsr).Then(response =>
        {
            Debug.Log("Update gameStatusRecords success");
            waitMechanism.SetOkToTrue();
        });
    }
    public static void UpdateGameStatusRecords()
    {
        GameStatusRecords[] gameStatusRecords = User.Instance.getGameStatusRecords();
        //GameStatusRecords[] gameStatusRecords = Test.GetGameStatusRecords();
        var str_json = "";
        for (int i = 0; i < gameStatusRecords.Length; i++)
        {
            str_json += JsonUtility.ToJson(gameStatusRecords[i]).ToString();
        }
        var data = JsonHelper.ArrayToJsonString(gameStatusRecords);

        string userId = User.Instance.userData.localId;

        RestClient.Put($"{databaseURL}users/{userId}/gameStatusRecords.json", data).Then(responseRecords =>
        {
            print("success update final records");
            FV_ButtonController.PostNewUserCompleted = true;
            waitMechanism.SetOkToTrue();
        });





        /*

        Queue GameStatusRecordsQueue = new Queue();
        GameStatusRecords[] allGameStatusRecords = new GameStatusRecords[5];
        RestClient.Get($"{databaseURL}users/{userId}/gameStatusRecords.json").Then(respons =>
        {
            var responseJson = respons.Text;
            allGameStatusRecords = JsonHelper.FromJsonString<GameStatusRecords>(responseJson);
            print(responseJson);
            //print(JSON.Parse(respons.Text).Count);
            /*for(int i = 0; i<5; i++)
            {

            }*/
            /*
            for (int i = 0; i < JSON.Parse(respons.Text).Count; i++)
            {
                var data = JSON.Parse(respons.Text)[i].ToString();
                print(data);
                allGameStatusRecords.Enqueue(data);
            }*//*
            GameStatusRecordsQueue.Enqueue(newGameStatusRecords);
            if (GameStatusRecordsQueue.Count >= 5)
            {
                GameStatusRecordsQueue.Dequeue();
            }

            string[] allGameStatusRecordsArr = new string[5];
            int count = GameStatusRecordsQueue.Count;
            print(count);
            for(int i = 0; i<count; i++)
            {
                allGameStatusRecordsArr[i] = GameStatusRecordsQueue.Dequeue().ToString();
                print(allGameStatusRecordsArr[i]);
            }
            var newallGameStatusRecords = JsonHelper.ArrayToJsonString(allGameStatusRecordsArr);
            RestClient.Put($"{databaseURL}users/{userId}/gameStatusRecords.json", newallGameStatusRecords).Then(responseRecords =>
            {
                print("success records");
            });
        }).Catch(Debug.LogWarning);
        */
        
    }
    public static void CreateGameStatusRecords(string userId)
    {
        GameStatusRecords[] nullGameStatusRecords = new GameStatusRecords[5];
        //string[] nullGameStatusRecords = new string[5];
        for(int i = 0; i<5; i++)
        {
            GameStatusRecords gameStatusRecords = new GameStatusRecords();
            
            nullGameStatusRecords[i] = gameStatusRecords;
            //nullGameStatusRecords.Enqueue("");
        }
        //print(nullGameStatusRecords.Count);
        var data = JsonHelper.ArrayToJsonString(nullGameStatusRecords);
        //var data = JsonUtility.ToJson(nullGameStatusRecords);
        
        RestClient.Put($"{databaseURL}users/{userId}/gameStatusRecords.json", data).Then(respons =>
        {
            print("Ok");
        }).Catch(Debug.LogWarning);
    }
}


