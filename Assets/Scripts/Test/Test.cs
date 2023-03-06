using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Material test = null;
    [SerializeField] Texture skin1 = null;
    Record[] records = new Record[5];
    [SerializeField] static GameStatusRecords[] gameStatusRecords = new GameStatusRecords[5];
    void Start()
    {

        //PostGameStatus();
        StartCoroutine("UpdateGameStatusRecords");
        /*GameObject.Find("T1").transform.Find("T2").gameObject.SetActive(false);
        GameObject.Find("T1").SetActive(false);
        test.mainTexture = skin1;
        for(int i = 0; i<5; i++)
        {
            records[i] = new Record();
            records[i].guest = i;
        }
        test2();*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void test2()
    {
        Record[] _records = new Record[5];
        for(int i = 0; i<5; i++)
        {
            _records[i] = records[i];
            print(_records[i].guest);
        }
    }
    public void PostGameStatus()
    {
        //DatabaseHandler.CreateGameStatusRecords("jUb7798EY0SSMKoZEevMVRZf3OL2");
        /*string test = "test";
        DatabaseHandler.UpdateGameStatusRecords("jUb7798EY0SSMKoZEevMVRZf3OL2", test);*/
    }

    public IEnumerator UpdateGameStatusRecords()
    {
        //GameStatusRecords[] allGameStatusRecords = new GameStatusRecords[5];
        for(int i = 0; i<5; i++)
        {
            gameStatusRecords[i] = new GameStatusRecords();
        }
        //DatabaseHandler.GetGameStatusRecords("jUb7798EY0SSMKoZEevMVRZf3OL2", gameStatusRecords);

        while (!waitMechanism.getOk())
        {
            yield return 0;
        }
        waitMechanism.SetOkToFalse();
        print("Get completed");
        //print(allGameStatusRecords.Length);
        int count = 0;
        string newTitle = "20201299";
        string newData = "QQQQ";
        int[] newOrder = new int[40];
        for (int j = 0; j < 40; j++)
        {
            newOrder[j] = j;
        }
        for (int i = 0; i<gameStatusRecords.Length; i++)
        {
            if (!gameStatusRecords[i].title.Equals(""))
            {
                count++;
            }
            else
            {
                
                gameStatusRecords[i].title = newTitle;
                break;
            }
            //print("title = " + gameStatusRecords[i].title);
        }
        print("count = " + count);
        
        if (count < 5)
        {
            DatabaseHandler.UpdateGameStatusRecords();
            while (!waitMechanism.getOk())
            {
                yield return 0;
            }
            waitMechanism.SetOkToFalse();
            print("Update success");
        }
        else
        {
            GameStatusRecords[] _gameStatusRecords = new GameStatusRecords[5];
            _gameStatusRecords[0] = new GameStatusRecords();
            _gameStatusRecords[0].title = newTitle;
            for(int i = 1; i<5; i++)
            {
                _gameStatusRecords[i] = new GameStatusRecords();
                _gameStatusRecords[i].title = gameStatusRecords[i].title;
            }
            SetGameStatusRecords(_gameStatusRecords);
            DatabaseHandler.UpdateGameStatusRecords();
            while (!waitMechanism.getOk())
            {
                yield return 0;
            }
            waitMechanism.SetOkToFalse();
            print("Update success");
        }
    }
    public static void SetGameStatusRecords(GameStatusRecords[] _gameStatusRecords)
    {
        gameStatusRecords = _gameStatusRecords;
    }
    public static GameStatusRecords[] GetGameStatusRecords()
    {
        return gameStatusRecords;
    }
}
