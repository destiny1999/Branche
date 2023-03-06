using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;

public class Lancher2 : MonoBehaviourPunCallbacks
{
    public static Lancher2 Instance;

    [SerializeField] TMP_InputField roomNameInputField = null;
    [SerializeField] TMP_Text errorText = null;
    [SerializeField] TMP_Text roomNameText = null;
    [SerializeField] Transform roomListContent = null;
    [SerializeField] GameObject roomListItemPrefab = null;
    [SerializeField] Transform playerListContent = null;
    [SerializeField] GameObject PlayerListItemPrefab = null;
    [SerializeField] GameObject startGameButton = null;
    [SerializeField] TMP_InputField playerNameInputField = null;

    static bool InRoom = false;
    static bool InFindRoom = false;

    [SerializeField] TMP_Text RegionText = null;

    static int count = 0;

    void Awake()
    {
        
        Instance = this;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //test
        /*PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = "test";*/


        if (!PhotonNetwork.NickName.Equals(""))
        {
            playerNameInputField.text = PhotonNetwork.NickName;
            ConnectToLobby();
        }
        else
        {
            MenuManager.Instance.OpenMenu("loading");
            PhotonNetwork.NickName = GameObject.Find("UserData").GetComponent<User>().userData.name;
            Debug.Log("Nickename = " + PhotonNetwork.NickName);
            ConnectToLobby();
        }
    }

    public void ConnectToLobby()
    {
        Debug.Log("Connectting to Lobby");
        if (!PhotonNetwork.IsConnected)
            //PhotonNetwork.ConnectToRegion("asia");
            PhotonNetwork.ConnectUsingSettings();
        else
        {
            if (InRoom)
            {
                InRoom = false;
                PhotonNetwork.LeaveRoom();
                //PhotonNetwork.JoinLobby();
                //MenuManager.Instance.OpenMenu("room");
            }
            else
            {
                PhotonNetwork.JoinLobby();
                Debug.Log("Connected - - -");
                MenuManager.Instance.OpenMenu("loading");
            }
            //LeaveRoom();
            
        }
        //PhotonNetwork.NickName = playerNameInputField.text;
        
    }

    public override void OnConnectedToMaster()
    {
        //465
        Debug.Log("Connected to master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("JoinedLobbyCallBack");
        if (!InFindRoom)
        {
            RegionText.text = PhotonNetwork.CloudRegion;
            
            //RegionText.text = PhotonNetwork.CloudRegion;
            MenuManager.Instance.OpenMenu("title");
            Debug.Log("Joined lobby");
        }
        else
        {
            MenuManager.Instance.OpenMenu("find room");
            InFindRoom = true;
        }

    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        InFindRoom = true;
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.Name.Equals("empty"))
        {
            return;
        }
        InRoom = true;
        MenuManager.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;
        
        foreach(Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }
        Debug.Log("PlayerCount  = " + players.Count());
        for (int i = 0; i < players.Count(); i++)
        {
            Debug.Log("Player name = " + players[i].NickName);
            Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public void JoinRoom(RoomInfo info)
    {
        if (info.PlayerCount < 4)
        {
            PhotonNetwork.JoinRoom(info.Name);
            MenuManager.Instance.OpenMenu("loading");
        }
    }
    public override void OnCreatedRoom()
    {
        if (PhotonNetwork.CurrentRoom.Name.Equals("empty"))
        {
            PhotonNetwork.LeaveRoom();
        }
        base.OnCreatedRoom();
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Failed: " + message;
        MenuManager.Instance.OpenMenu("error");
    }

    public void StartGame()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount < 4 )
        {
            print("Player not enough! Now only " + PhotonNetwork.CurrentRoom.PlayerCount);
        }
        else
        {
            PhotonNetwork.LoadLevel("3Dgame2");
        }
    }
    public void Ready()
    {
        // not implement;
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom");
        count = 0;
        InRoom = false;
        MenuManager.Instance.OpenMenu("find room"); 
    }
    
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        count++;
        if (count == 2)
        {
            count = 0;
            return;
        }
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }

        Debug.Log("List count = " + roomList.Count);
        Debug.Log("Photon room count = " + PhotonNetwork.CountOfRooms);

        for(int i = 0; i<roomList.Count; i++)
        {
            Debug.Log(roomList[i].Name + " esist");
            if (roomList[i].RemovedFromList)
            {
                Debug.Log(roomList[i].Name + " close");
                continue;
            }
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);         
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public void LeaveLobby()
    {
        PhotonNetwork.LeaveLobby();    
    }
    public override void OnLeftLobby()
    {
        Debug.Log("Left lobby");
        InFindRoom = false;
        DontDestroyOnLoad(GameObject.Find("UserData"));
        //DontDestroyOnLoad(GameObject.Find("AudioSetting"));
        UnityEngine.SceneManagement.SceneManager.LoadScene("SelectMode");   
    }
    public void UpdateRoomList()
    {
        InFindRoom = true;
        PhotonNetwork.CreateRoom("empty");
        MenuManager.Instance.OpenMenu("loading");
    }
}
