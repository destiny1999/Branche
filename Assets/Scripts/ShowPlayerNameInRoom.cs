using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ShowPlayerNameInRoom : MonoBehaviour
{
    private static int playerNum;
    public string[] PlayerName = new string[4];
    // Start is called before the first frame update
    void Start()
    {
        playerNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerNum != PhotonNetwork.PlayerList.Length)
        {
            GameObject[] PlayerNameFiled = { GameObject.Find("P1").gameObject,
                                            GameObject.Find("P2").gameObject,
                                            GameObject.Find("P3").gameObject,
                                            GameObject.Find("P4").gameObject};

            for(int i= 0; i<4; i++)
            {
                string name;
                try
                {
                    name = PhotonNetwork.PlayerList[i].NickName;
                }
                catch
                {
                    name = "";
                }
                Debug.Log("name is " + name);
                PlayerName[i] = name;
                
                PlayerNameFiled[i].transform.Find("Name").GetComponent<UnityEngine.UI.Text>().text = name;
            }
            playerNum = PhotonNetwork.PlayerList.Length;
            if(playerNum == 4)
            {
                Debug.Log("All four player are ready");
                //SceneManager.LoadScene(2);              
            }
            else
            {
                Debug.Log("Now Player is " + playerNum);
            }
            
            
        }
    }
}
