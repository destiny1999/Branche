using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ExitGames.Client.Photon;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class GameManager2 : PunTurnManager, IPunTurnManagerCallbacks
{
    int turn = 0;
    public int PlayerCount = 4;
    static int ready = 0;
    bool wait = true;
    bool completed = false;
    static string sendTurnEndName = "";
    static int gameStart = 0;
    bool started = false;
    static bool changeAction = false;

    static int buildUseWho = -1; // 0 mean self 1 mean public 2 mean other
    static int buildPos = -1;

    [SerializeField] GameObject Deck = null;

    Deck2 deck = Deck2.Instance;

    public static Queue<GuestCardData2> GuestCard;
    public PlayerData2 PlayerData;
    
    // 1 1 mean player1 steaker 1 2 mean player1 baker

    //1 steak 2 cake 3 juice 4 chef 5 build 6 clear 7 welcome
    [SerializeField] GameObject CharacterSteaker = null;
    [SerializeField] GameObject CharacterBaker = null;
    [SerializeField] GameObject CharacterBar = null;
    [SerializeField] GameObject CharacterChef = null;
    [SerializeField] GameObject CharacterBuilder = null;
    [SerializeField] GameObject CharacterCleaner = null;
    [SerializeField] GameObject CharacterWelcomer = null;

    [SerializeField] GameObject Player1Guest1 = null;
    [SerializeField] GameObject Player1Guest2 = null;
    [SerializeField] GameObject Player1Guest3 = null;
    [SerializeField] GameObject Player1Guest4 = null;
    [SerializeField] GameObject Player1CharacterPos = null;

    [SerializeField] GameObject Player2Guest1 = null;
    [SerializeField] GameObject Player2Guest2 = null;
    [SerializeField] GameObject Player2Guest3 = null;
    [SerializeField] GameObject Player2Guest4 = null;
    [SerializeField] GameObject Player2CharacterPos = null;

    [SerializeField] GameObject Player3Guest1 = null;
    [SerializeField] GameObject Player3Guest2 = null;
    [SerializeField] GameObject Player3Guest3 = null;
    [SerializeField] GameObject Player3Guest4 = null;
    [SerializeField] GameObject Player3CharacterPos = null;

    [SerializeField] GameObject Player4Guest1 = null;
    [SerializeField] GameObject Player4Guest2 = null;
    [SerializeField] GameObject Player4Guest3 = null;
    [SerializeField] GameObject Player4Guest4 = null;
    [SerializeField] GameObject Player4CharacterPos = null;

    [SerializeField] GameObject Player1PlatePos = null;
    [SerializeField] GameObject Player2PlatePos = null;
    [SerializeField] GameObject Player3PlatePos = null;
    [SerializeField] GameObject Player4PlatePos = null;

    [SerializeField] GameObject Player1PlateCount = null;
    [SerializeField] GameObject Player2PlateCount = null;
    [SerializeField] GameObject Player3PlateCount = null;
    [SerializeField] GameObject Player4PlateCount = null;

    [SerializeField] GameObject Plates = null;

    [SerializeField] GameObject Player1 = null;
    [SerializeField] GameObject Player2 = null;
    [SerializeField] GameObject Player3 = null;
    [SerializeField] GameObject Player4 = null;

    [SerializeField] Sprite steak = null;
    [SerializeField] Sprite cake = null;
    [SerializeField] Sprite juice = null;
    [SerializeField] Sprite UIMask = null;

    [SerializeField] Sprite two = null;
    [SerializeField] Sprite three = null;
    [SerializeField] Sprite four = null;
    [SerializeField] Sprite five = null;
    [SerializeField] Sprite six = null;
    [SerializeField] Sprite seven = null;
    [SerializeField] Sprite eight = null;
    [SerializeField] Sprite nine = null;
    [SerializeField] Sprite ten = null;

    [SerializeField] GameObject Player1Name = null;
    [SerializeField] GameObject Player2Name = null;
    [SerializeField] GameObject Player3Name = null;
    [SerializeField] GameObject Player4Name = null;

    [SerializeField] GameObject Player1Money = null;
    [SerializeField] GameObject Player2Money = null;
    [SerializeField] GameObject Player3Money = null;
    [SerializeField] GameObject Player4Money = null;

    [SerializeField] GameObject Player1Mark = null;
    [SerializeField] GameObject Player2Mark = null;
    [SerializeField] GameObject Player3Mark = null;
    [SerializeField] GameObject Player4Mark = null;
    [SerializeField] GameObject Player1Bonuse = null;
    [SerializeField] GameObject Player2Bonuse = null;
    [SerializeField] GameObject Player3Bonuse = null;
    [SerializeField] GameObject Player4Bonuse = null;

    [SerializeField] Sprite SteakerSprite = null;
    [SerializeField] Sprite BakerSprite = null;
    [SerializeField] Sprite BarSprite = null;
    [SerializeField] Sprite ChefSprite = null;
    [SerializeField] Sprite BuilderSprite = null;
    [SerializeField] Sprite CleanerSprite = null;
    [SerializeField] Sprite WelcomerSprite = null;

    [SerializeField] Sprite SteakerIcon = null;
    [SerializeField] Sprite BakerIcon = null;
    [SerializeField] Sprite BarIcon = null;
    [SerializeField] Sprite ChefIcon = null;
    [SerializeField] Sprite BuilderIcon = null;
    [SerializeField] Sprite CleanerIcon = null;
    [SerializeField] Sprite WelcomerIcon = null;

    [SerializeField] GameObject Player1CharacterIcon = null;
    [SerializeField] GameObject Player2CharacterIcon = null;
    [SerializeField] GameObject Player3CharacterIcon = null;
    [SerializeField] GameObject Player4CharacterIcon = null;


    [SerializeField] GameObject PublicCharacter1 = null;
    [SerializeField] GameObject PublicCharacter2 = null;
    [SerializeField] GameObject PublicCharacter3 = null;

    /*
    [SerializeField] GameObject ChangeButton1 = null;
    [SerializeField] GameObject ChangeButton2 = null;
    [SerializeField] GameObject ChangeButton3 = null;*/

    [SerializeField] GameObject Support = null;
    [SerializeField] GameObject SupportButton1 = null;
    [SerializeField] GameObject SupportButton2 = null;
    [SerializeField] GameObject SupportButton3 = null;




    [SerializeField] GameObject TurnMessage = null;
    [SerializeField] GameObject ControlButton = null;

    [SerializeField] GameObject SteakObject = null;
    [SerializeField] GameObject CakeObject = null;
    [SerializeField] GameObject JuiceObject = null;

    [SerializeField] GameObject ActionDetect = null;
    [SerializeField] GameObject UsePlayer = null;

    [SerializeField] GameObject BonuseMoney = null;
    [SerializeField] GameObject TableMoney = null;

    [SerializeField] GameObject EndTurn = null;
    [SerializeField] GameObject FinalView = null;
    [SerializeField] GameObject Order1 = null;
    [SerializeField] GameObject Order2 = null;
    [SerializeField] GameObject Order3 = null;
    [SerializeField] GameObject Order4 = null;

    [SerializeField] GameObject InsufficientBalance = null;
    [SerializeField] GameObject TurnTable_Player1 = null;
    [SerializeField] GameObject TurnTable_Player2 = null;
    [SerializeField] GameObject TurnTable_Player3 = null;
    [SerializeField] GameObject TurnTable_Player4 = null;
    [SerializeField] Material Red = null;
    [SerializeField] Material Blue = null;
    [SerializeField] Material Green = null;
    [SerializeField] Material Yellow = null;
    [SerializeField] Material Gray = null;

    [SerializeField] Material Player1TableMaterial = null;
    [SerializeField] Material Player2TableMaterial = null;
    [SerializeField] Material Player3TableMaterial = null;
    [SerializeField] Material Player4TableMaterial = null;

    [SerializeField] Texture[] TableSkin = null;

    //[SerializeField] TMP_Text selfMoney = null;
    //[SerializeField] TMP_Text status = null;
    [SerializeField] TMP_Text GuestCount = null;

    PhotonView photonView = null;

    Dictionary<int, string> PlayerOrder = new Dictionary<int, string>();
    Dictionary<int, int> PlayerMoney = new Dictionary<int, int>();
    public Dictionary<int, int> CharacterPos = new Dictionary<int, int>(); // use index(pos) to know which character

    public static GameManager2 Instance;

    public static bool end = false;
    static bool checkFinished = false;


    GameObject[] AllPlayer;
    GameObject[] AllPublicCharacter;
    GameObject[] AllCharacter;
    GameObject[] AllPlatePos;
    GameObject[] AllPlateCount;
    GameObject[] AllUsePlayer;
    GameObject[] AllSupportButton;
    Sprite[] AllCharacterIcon;
    GameObject[] AllPlayerCharacterIcon;
    GameObject[] AllPlayerMoney;
    GameObject[] AllPlayerGuest;
    GameObject[] AllPlayerName;
    GameObject[] AllOrder;
    GameObject[] AllTurnTable;
    Material[] AllTurnTableMaterial;
    Material[] AllPlayerTableMaterial;

    Sprite[] AllCharacterSprite;

    List<string[]> FinalData = new List<string[]>();

    private ClickObect clickObect;

    
    #region PunturnManagerCallBack
    public void OnPlayerFinished(Player player, int turn, object move)
    {
        throw new System.NotImplementedException();
    }

    public void OnPlayerMove(Player player, int turn, object move)
    {
        throw new System.NotImplementedException();
    }

    public void OnTurnBegins(int turn)
    {
        throw new System.NotImplementedException();
    }

    public void OnTurnCompleted(int turn)
    {
        throw new System.NotImplementedException();
    }

    public void OnTurnTimeEnds(int turn)
    {
        throw new System.NotImplementedException();
    }
    #endregion
    void Awake()
    {
        Instance = this;
        clickObect = GetComponent<ClickObect>();

    }
    void Update()
    {
        //Debug.Log("gameStart = " + gameStart);
        GuestCount.text = GuestCard.Count + "";
        //selfMoney.text = PlayerData.Money + "";
        
        if(PhotonNetwork.IsMasterClient && gameStart == PlayerCount && !started)
        {
            //turnt start;
            started = true;
            photonView.RPC("TurnStart", RpcTarget.All, turn);
        }
        //Debug.Log("Test started " + started);
        /*if (started && PhotonNetwork.NickName.Equals(PlayerOrder[turn % PlayerCount + 1]))
        {
            //selfMoney.text = ActionController.Instance.GetAction() + "";
        }*/
        if (PhotonNetwork.IsMasterClient && completed && wait)
        {
            Debug.Log("ready = " + ready);
            if (ready != PlayerCount)
            {
                Debug.Log("wait...");
            }
            else
            {
                wait = false;

                int[] ShuffleOGC = deck.Shuffle(1, 10);
                int[] ShuffleGC = deck.Shuffle(11, 40);

                int[] ShuffleCharacter = deck.Shuffle(1, 7);
                for(int i = 0; i<7; i++)
                {
                    CharacterPos.Add(i+1, ShuffleCharacter[i]);
                }

                SetOrder();
                string[] nameOrder = new string[PlayerCount];
                for (int i = 1; i <= PlayerCount; i++)
                {
                    nameOrder[i - 1] = PlayerOrder[i];
                }
                deck.SetUp(ShuffleOGC, ShuffleGC);
                GuestCard = deck.getGuestCard();
                photonView.RPC("SendDataToOther", RpcTarget.Others, ShuffleOGC, ShuffleGC, ShuffleCharacter,nameOrder);

                

                GameObject[] PlayerName = { Player1Name, Player2Name, Player3Name, Player4Name };
                GameObject[] PlayerMoney = { Player1Money, Player2Money, Player3Money, Player4Money };

                for (int i = 1; i <= PlayerCount; i++)
                {
                    photonView.RPC("SetPlayerData", RpcTarget.All, PlayerOrder[i], i);
                    photonView.RPC("Prepare", RpcTarget.All, i);
                }
            }
        }
        /*if (PhotonNetwork.PlayerList.Length != PlayerCount)
        {
            Application.Quit();
        }*/
        if(end && PhotonNetwork.IsMasterClient && FinalData.Count == PlayerCount)
        {
            //print("To Process Final Data");
            ProcessFinalData();
            end = false;
        }
        
    }
    
    void Start()
    {
        Debug.Log("Test into Start");

        buildPos = -1;
        buildUseWho = -1;
        turn = 0;
        ready = 0;
        wait = true;
        completed = false;
        sendTurnEndName = "";
        gameStart = 0;
        started = false;
        changeAction = false;
        end = false;
        checkFinished = false;
    // / / / / / / / / / /
    clickObect.enabled = !clickObect.enabled;
        #region ArrayData
        GameObject[] _AllPlayer = { Player1, Player2, Player3, Player4 };
        GameObject[] _AllPublicCharacter = { PublicCharacter1, PublicCharacter2, PublicCharacter3 };
        GameObject[] _AllCharacter = {CharacterSteaker, CharacterBaker,CharacterBar,
                                    CharacterChef, CharacterBuilder,CharacterCleaner,
                                    CharacterWelcomer};
        GameObject[] _AllPlatePos = { Player1PlatePos, Player2PlatePos, Player3PlatePos, Player4PlatePos };
        GameObject[] _AllPlateCount = { Player1PlateCount, Player2PlateCount, Player3PlateCount, Player4PlateCount };
        GameObject[] _AllUsePlayer = {UsePlayer.transform.Find("P1").gameObject,
                                    UsePlayer.transform.Find("P2").gameObject,
                                    UsePlayer.transform.Find("P3").gameObject,
                                    UsePlayer.transform.Find("P4").gameObject};
        GameObject[] _AllSupportButton = { SupportButton1, SupportButton2, SupportButton3 };
        Sprite[] _AllCharacterIcon = {SteakerIcon, BakerIcon, BarIcon,ChefIcon,BuilderIcon,
                                        CleanerIcon, WelcomerIcon};
        GameObject[] _AllPlayerCharacterIcon = {Player1CharacterIcon, Player2CharacterIcon,
                                        Player3CharacterIcon, Player4CharacterIcon};
        GameObject[] _AllPlayerMoney = { Player1Money, Player2Money, Player3Money, Player4Money };

        GameObject[] _AllPlayerGuest = { Player1Guest1, Player1Guest2, Player1Guest3, Player1Guest4,
                                     Player2Guest1, Player2Guest2, Player2Guest3, Player2Guest4,
                                     Player3Guest1, Player3Guest2, Player3Guest3, Player3Guest4,
                                     Player4Guest1, Player4Guest2, Player4Guest3, Player4Guest4};
        GameObject[] _AllPlayerName = { Player1Name, Player2Name, Player3Name, Player4Name };
        GameObject[] _AllOrder = { Order1, Order2, Order3, Order4 };
        GameObject[] _AllTurnTable = { TurnTable_Player1, TurnTable_Player2,
                                        TurnTable_Player3, TurnTable_Player4};
        Material[] _AllTurnTableMaterial = { Gray, Red, Blue, Green, Yellow };

        Material[] _AllPlayerTableMaterial = {Player1TableMaterial, Player2TableMaterial,
                                            Player3TableMaterial, Player4TableMaterial};

        Sprite[] _AllCharacterSprite = {SteakerSprite, BakerSprite, BarSprite, ChefSprite,
                                        BuilderSprite, CleanerSprite, WelcomerSprite};

        AllPlayer = _AllPlayer;
        AllPublicCharacter = _AllPublicCharacter;
        AllCharacter = _AllCharacter;
        AllPlatePos = _AllPlatePos;
        AllPlateCount = _AllPlateCount;
        AllUsePlayer = _AllUsePlayer;
        AllSupportButton = _AllSupportButton;
        AllCharacterIcon = _AllCharacterIcon;
        AllPlayerCharacterIcon = _AllPlayerCharacterIcon;
        AllPlayerMoney = _AllPlayerMoney;
        AllPlayerGuest = _AllPlayerGuest;
        AllPlayerName = _AllPlayerName;
        AllOrder = _AllOrder;
        AllTurnTable = _AllTurnTable;
        AllTurnTableMaterial = _AllTurnTableMaterial;
        AllPlayerTableMaterial = _AllPlayerTableMaterial;

        AllCharacterSprite = _AllCharacterSprite;

        #endregion
        ready = 0;
        Deck.SetActive(true);
        deck = Deck2.Instance;
        photonView = this.GetComponent<PhotonView>();

        PlayerData = new PlayerData2();
        GuestCard = new Queue<GuestCardData2>();
        CharacterPos = new Dictionary<int, int>();

        deck.Create();
        PlayerMoney.Add(1, 5);
        PlayerMoney.Add(2, 5);
        PlayerMoney.Add(3, 6);
        PlayerMoney.Add(4, 6);

        

        if (!PhotonNetwork.IsMasterClient)
        {             
            photonView.RPC("Ready", RpcTarget.MasterClient);
            started = true;
        }
        completed = true;
        ready++;
    }
    
    void SetOrder()
    {
        int[] order = Deck2.Instance.Shuffle(1, PlayerCount);
        int index = 0;
        foreach(Player playerName in PhotonNetwork.PlayerList)
        {
            PlayerOrder.Add(order[index], playerName.NickName);
            index++;
        }

    }

    public void ClearGuestData(GameObject PlayerGuest)
    {

        // here let all can't see
        // if set new card it will don't show the redundant items and set new data
        PlayerGuest.transform.Find("people1").gameObject.SetActive(false);
        PlayerGuest.transform.Find("people2").gameObject.SetActive(false);
        PlayerGuest.transform.Find("people3").gameObject.SetActive(false);

        PlayerGuest.transform.Find("money").gameObject.SetActive(false);

        PlayerGuest.transform.Find("items").Find("1").Find("1").gameObject.SetActive(false);
        PlayerGuest.transform.Find("items").Find("1").Find("2").gameObject.SetActive(false);
        PlayerGuest.transform.Find("items").Find("1").Find("3").gameObject.SetActive(false);
        PlayerGuest.transform.Find("items").Find("1").Find("4").gameObject.SetActive(false);

        PlayerGuest.transform.Find("items").Find("2").Find("1").gameObject.SetActive(false);
        PlayerGuest.transform.Find("items").Find("2").Find("2").gameObject.SetActive(false);
        PlayerGuest.transform.Find("items").Find("2").Find("3").gameObject.SetActive(false);
        PlayerGuest.transform.Find("items").Find("2").Find("4").gameObject.SetActive(false);

        PlayerGuest.transform.Find("items").Find("3").Find("1").gameObject.SetActive(false);
        PlayerGuest.transform.Find("items").Find("3").Find("2").gameObject.SetActive(false);
        PlayerGuest.transform.Find("items").Find("3").Find("3").gameObject.SetActive(false);
        PlayerGuest.transform.Find("items").Find("3").Find("4").gameObject.SetActive(false);

        GameObject[] people = { PlayerGuest.transform.Find("items").Find("1").gameObject,
                            PlayerGuest.transform.Find("items").Find("2").gameObject,
                            PlayerGuest.transform.Find("items").Find("3").gameObject};

        for (int i = 0; i < people.Length; i++)
        {
            GameObject[] foodIndex = {people[i].transform.Find("1").gameObject,
                                    people[i].transform.Find("2").gameObject,
                                    people[i].transform.Find("3").gameObject,
                                    people[i].transform.Find("4").gameObject };

            for (int j = 0; j < foodIndex.Length; j++)
            {
                if (foodIndex[j].transform.childCount > 0)
                {
                    Destroy(foodIndex[j].transform.Find("Food").gameObject);
                }
            }
        }
        waitMechanism.SetOkToTrue();
    }
    public void SetGuestData(GameObject PlayerGuest, GuestCardData2 info)
    {
        PlayerGuest.SetActive(true);
        PlayerGuest.transform.Find("money").gameObject.SetActive(true);
        Sprite[] MoneySprite = { two, three, four, five, six, seven, eight, nine, ten };
        int money = info.Money;
        PlayerGuest.transform.Find("money").GetComponent<SpriteRenderer>().sprite = MoneySprite[money - 2];
        //Debug.Log("SetGuestData " + PlayerGuest.name + " CardInfo " + info.CardName);
        PlayerGuest.transform.Find("table").gameObject.SetActive(true);
        int people = info.People;
        GameObject[] People = { PlayerGuest.transform.Find("people1").gameObject,
                                PlayerGuest.transform.Find("people2").gameObject,
                                PlayerGuest.transform.Find("people3").gameObject};
        GameObject[] items = { PlayerGuest.transform.Find("items").Find("1").gameObject,
                                PlayerGuest.transform.Find("items").Find("2").gameObject,
                                PlayerGuest.transform.Find("items").Find("3").gameObject};

        List<Queue> ItemList = info.ItemList;

        for (int i = 0; i < people; i++)
        {
            People[i].SetActive(true);
            items[i].SetActive(true);
            Queue foods = ItemList[i];
            int index = 1;
            foreach (int food in foods)
            {
                //Debug.Log(food);
                string which = index + "";
                items[i].transform.Find(which).GetComponent<SpriteRenderer>().gameObject.SetActive(true);
                if (food == 1)
                {
                    items[i].transform.Find(which).GetComponent<SpriteRenderer>().sprite = steak;
                }
                else if (food == 2)
                {
                    items[i].transform.Find(which).GetComponent<SpriteRenderer>().sprite = cake;
                }
                else if (food == 3)
                {
                    items[i].transform.Find(which).GetComponent<SpriteRenderer>().sprite = juice;
                }
                index++;
            }
        }

    }

    #region RPC Methods
    //- - - - - - - - -Turn
    #region RPC SetUp


    //- - - - - - - - -SetUp 
    [PunRPC]
    public void Ready()
    {
        ready++;
    }


    [PunRPC]
    void SendDataToOther(int[] ShuffleOGC , int[] ShuffleGC, int[] ShuffleCharacter, string[] playerOrder)
    {
        
        deck.SetUp(ShuffleOGC, ShuffleGC);
        GuestCard = deck.getGuestCard();

        

        for (int i = 0; i<7; i++)
        {
            CharacterPos.Add(i + 1, ShuffleCharacter[i]);
        }
        //Debug.Log("test1");
        for(int i = 1; i<=PlayerCount; i++)
        {
            PlayerOrder.Add(i, playerOrder[i - 1]);
        }
        
        //Debug.Log("test2");
    }

    [PunRPC]
    void SetPlayerData(string _name, int index) // 1 2 3 4 
    {
        //Debug.Log("SetPlayerData");
        AllPlayerName[index - 1].GetComponent<TextMeshProUGUI>().text = PlayerOrder[index];

        int moneyInfo = index <= PlayerCount / 2 ? 5 : 6;
        AllPlayerMoney[index - 1].GetComponent<TextMeshProUGUI>().text = moneyInfo + "";

        if (PhotonNetwork.NickName.Equals(_name))
        {
            int skinIndex = GameObject.Find("UserData").GetComponent<User>().userData.deskColorIndex;
            int money = index < 3 ? 5:6 ;
            string name = _name;
            int moneyBonuse = 0;
            int guestCapacity = 2;
            int dishes = 0;
            PlayerData = new PlayerData2(money, name, dishes, moneyBonuse, new List<GuestCardData2>(), guestCapacity);
            AllPlayerTableMaterial[index - 1].mainTexture =
                TableSkin[skinIndex];
            photonView.RPC("setTableSkin", RpcTarget.Others, index, skinIndex);
        }
    }
    [PunRPC]
    void setTableSkin(int index, int skinIndex)
    {
        AllPlayerTableMaterial[index - 1].mainTexture =
                TableSkin[skinIndex];
    }
    [PunRPC]
    void Prepare(int index) // 1-01    2-23    3-45   4-67
    {
        GameObject[] PlayerCharacterPos = {Player1CharacterPos, Player2CharacterPos, Player3CharacterPos,
                                    Player4CharacterPos};
        GameObject[] Character = {CharacterSteaker, CharacterBaker, CharacterBar,
                                  CharacterChef, CharacterBuilder, CharacterCleaner,
                                  CharacterWelcomer};
        GameObject[] Player = { Player1, Player2, Player3, Player4 };


        int who = CharacterPos[index];
        AllPlayerCharacterIcon[index - 1].GetComponent<UnityEngine.UI.Image>().sprite = AllCharacterIcon[who - 1];

        GameObject character = Instantiate(Character[who - 1],
            PlayerCharacterPos[index-1].transform.position, PlayerCharacterPos[index-1].transform.rotation);
        character.transform.SetParent(Player[index - 1].transform);
        character.name = "Character";
        //character.transform.localScale = new Vector3(250, 250, 250);

        

        GameObject[] PlayerGuest = { Player1Guest1, Player1Guest2,
                                     Player2Guest1, Player2Guest2,
                                     Player3Guest1, Player3Guest2,
                                     Player4Guest1, Player4Guest2};



        for(int j = 0; j<2; j++)
        {
            GuestCardData2 card = GuestCard.Dequeue();
            //Debug.Log("index = " + index);
            
            if (PlayerOrder[index].Equals(PlayerData.Name))
            {
                PlayerData.CardList.Add(card);
            }
            ClearGuestData(PlayerGuest[j+(index-1)*2]);
            SetGuestData(PlayerGuest[j+(index-1)*2], card);
        }
        if(index == PlayerCount)
        {
            Sprite[] AllRoleSprite = {SteakerSprite, BakerSprite, BarSprite,
                                    ChefSprite, BuilderSprite, CleanerSprite, WelcomerSprite};
            GameObject[] PublicAreaSprite = { PublicCharacter1, PublicCharacter2, PublicCharacter3 };
            for(int i = 0; i<3; i++)
            {
                PublicAreaSprite[i].GetComponent<UnityEngine.UI.Image>().sprite =
                    AllRoleSprite[CharacterPos[i + 5]-1];
            }
            photonView.RPC("AddGameStart", RpcTarget.MasterClient);
        }
        if(index == PlayerCount)
        {
            while (GuestCard.Count > 30)
            {
                GuestCard.Dequeue();
            }
        }
    }
    [PunRPC]
    void AddGameStart()
    {
        gameStart++;
    }
    #endregion
    #region RPC Turn
    [PunRPC]
    public void TurnStart(int _turn)
    {
        
        //Debug.Log("Test who call me");
        //status.text = "";
        changeAction = false;
        turn = _turn;
        /*if (checkFinished && PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Check Player = " + (turn % PlayerCount + 1));
            if(turn % PlayerCount + 1 == 1)
            {
                Debug.Log("Fianl");
                photonView.RPC("CallGameFinished", RpcTarget.All);
            }
        }*/

        if (checkFinished)
        {
            if(turn % PlayerCount +1 == 1)
            {
                /*Record[] records = new Record[5];
                Record[] _records = new Record[5];
                _records = User.Instance.getRecords();
                records[0].
                for(int i = 1; i<5; i++)
                {
                    records[i] = 
                }*/
                if (PhotonNetwork.IsMasterClient)
                {
                    photonView.RPC("CallGameFinished", RpcTarget.All);
                }
                else
                {
                    return;
                }
            }

            /*if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("Check Player = " + (turn % PlayerCount + 1));
                if (turn % PlayerCount + 1 == 1)
                {
                    Debug.Log("Fianl");
                    photonView.RPC("CallGameFinished", RpcTarget.All);
                }
                else
                {
                    // should let the turn continue becuase the return let other can't do
                    // anything
                }
            }
            return;*/
        }

        EndTurn.SetActive(false);
           
        
        GameMethods2.Instance.ChangeTurnTable(PlayerCount, turn, AllTurnTable, AllTurnTableMaterial);
        string playerName = PlayerOrder[turn % PlayerCount + 1];
        TurnMessage.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = playerName + "'s Turn";
        TurnMessage.SetActive(true);
        
        if (PhotonNetwork.NickName.Equals(playerName))
        {
            GameMethods2.Instance.SetAllPlayerAbilityCanUse(AllUsePlayer);
            ActionDetect.SetActive(true);
            ActionController.Instance.SetTimesToZero();
            EndTurn.SetActive(true);
            ControlButton.SetActive(true);
            ControlButton.transform.Find("Call").gameObject.SetActive(true);
            //Debug.Log("Start Action = " + ActionController.Instance.GetAction());
        }
    }
    [PunRPC]
    public void NextTurn(string name)
    {
        //Debug.Log("Call by " + name);
        if (name.Equals(sendTurnEndName))
        {
            return;
        }
        //Debug.Log("Next Turn");
        sendTurnEndName = name;
        if (PhotonNetwork.IsMasterClient)
        {
            turn++;
            photonView.RPC("TurnStart", RpcTarget.All, turn);
        }
    }
    [PunRPC]
    public void CallGameFinished()
    {
        //photonView.RPC()
        //Debug.Log("I'm MasterClient I'm coming!!!!!!");
        string name = PlayerData.Name;
        int money = PlayerData.Money;
        int plates = PlayerData.Dishes;
        int bonuse = PlayerData.MoneyBonus * PlayerData.GuestCapacity;
        int score = money / 2 + bonuse - PlayerData.Dishes;
        end = true;
        
        Record[] records = new Record[5];
        Record[] _records = new Record[5];

        GameStatusRecords[] gameStatusRecords = new GameStatusRecords[5];
        GameStatusRecords[] _gameStatusRecords = new GameStatusRecords[5];
        for (int i = 0; i<5; i++)
        {
            _records[i] = new Record();
            records[i] = new Record();

            _gameStatusRecords[i] = new GameStatusRecords();
            gameStatusRecords[i] = new GameStatusRecords();
        }
        /*if (User.Instance.getRecords().Length != 0)
        {
            for(int i = 0; i<User.Instance.getRecords().Length; i++)
            {
                records[i] = User.Instance.getRecords()[i];
            }
        };*/
        _gameStatusRecords[0].title = DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "";
        _gameStatusRecords[0].data = LogInfoController.Instance.getLogInfo().text.ToString();

        _records[0].bonuse = PlayerData.MoneyBonus;
        _records[0].score = score;
        _records[0].date = DateTime.Now.Year+"/"+DateTime.Now.Month+"/"+DateTime.Now.Day+"";
        _records[0].guest = PlayerData.GuestCapacity;
        _records[0].plate = PlayerData.Dishes;
        records = User.Instance.getRecords();
        gameStatusRecords = User.Instance.getGameStatusRecords();
        for(int i = 1; i<records.Length; i++)
        {
            //print("i = " + i);
            _records[i] = records[i - 1];
            _gameStatusRecords[i] = gameStatusRecords[i - 1];
        }
        
        User.Instance.setFinalRecords(_records);
        User.Instance.setGameStatusRecords(_gameStatusRecords);
        //Debug.Log("SetNewOK");
        photonView.RPC("SendFinialDataToMaster", RpcTarget.MasterClient,name, money, plates, bonuse, score);
    }
    [PunRPC]
    public void CallShowLastRound()
    {
        checkFinished = true;
    }
    [PunRPC]
    public void SendFinialDataToMaster(string name, int money, int plates, int bonuse, int score)
    {
        //print("Recieve " + name);
        string[] finalInfo = new string[5];
        finalInfo[0] = name;
        finalInfo[1] = money+"";
        finalInfo[2] = plates+"";
        finalInfo[3] = bonuse+"";
        finalInfo[4] = score+"";

        FinalData.Add(finalInfo);
    }
    [PunRPC]
    public void CallShowFinalInfo(int[] order , string finalData)
    {
        var bf = new BinaryFormatter();
        var ins = new MemoryStream(Convert.FromBase64String(finalData));
        List<string[]> data =(List<string[]>) bf.Deserialize(ins);
        
        GameMethods2.Instance.ShowFinalInfo(AllOrder, data, order);
        FinalView.SetActive(true);
    }
    #endregion
    #region RPC Change Show Data
    [PunRPC]
    public void CallChangeRole(int which)
    {
        string[] allRoleName = {"Steaker", "Baker", "Bar",
                                    "Chef", "Builder", "Cleaner", "Receptionist"};

        int player = turn % PlayerCount + 1;
        int playerCharacterCode = CharacterPos[player];
        
        int targetCode = CharacterPos[which];
        Sprite[] AllRoleSprite = {SteakerSprite, BakerSprite, BarSprite,
                                    ChefSprite, BuilderSprite, CleanerSprite, WelcomerSprite};

        string oldRole = allRoleName[playerCharacterCode-1];
        string newRole = allRoleName[targetCode-1];

        GameMethods2.Instance.ChangePublicAreaRoleSprite(AllPublicCharacter[which - 5], 
            AllRoleSprite[playerCharacterCode-1]);
        Destroy(AllPlayer[player - 1].transform.Find("Character").gameObject);
        GameMethods2.Instance.ChangeRole(AllPlayer[player-1], AllCharacter[targetCode-1]);
        changeAction = true;
        AllPlayerCharacterIcon[player - 1].GetComponent<UnityEngine.UI.Image>().sprite =
                AllCharacterIcon[targetCode - 1];

        CharacterPos[player] = targetCode;
        CharacterPos[which] = playerCharacterCode;

        string playerName = PlayerOrder[turn % PlayerCount + 1];

        GameMethods2.Instance.AddChangeRoleInfo(playerName, oldRole, newRole);
        //checkFinished = true;
    }
    [PunRPC]
    public void CallSupportUse(int which)
    {

    }
    [PunRPC]
    public void CallCreateFood(string data, int abilityCode)
    {
        Debug.Log("code = " + abilityCode);
        var bf = new BinaryFormatter();
        var ins = new MemoryStream(Convert.FromBase64String(data)); 
        ArrayList createPos = (ArrayList)bf.Deserialize(ins);

        GameObject[] allFoods = { SteakObject, CakeObject, JuiceObject };
        int player = turn % PlayerCount + 1;
        GameMethods2.Instance.CreateFood(AllPlayer[player - 1], createPos,
            allFoods[abilityCode - 1], abilityCode, UIMask);
        
    }
    [PunRPC]
    public void CallBuildUse(int which)
    {
        int player = turn % PlayerCount + 1;
        string playerName = PlayerOrder[player];
        if (which == 1)
        {
            //if bonuse !=3
            GameObject[] allBonuse = { Player1Bonuse, Player2Bonuse, Player3Bonuse, Player4Bonuse };
            GameObject target = allBonuse[player - 1];
            GameMethods2.Instance.BonuseLevelUp(target, playerName, PlayerData.Name);
        }
        else if(which == 2)
        {
            //string playerName = PlayerOrder[player];
            GameObject[] allPlayerMark = { Player1Mark, Player2Mark, Player3Mark, Player4Mark };
            GameMethods2.Instance.CreateTable(AllPlayer[player - 1]);
            GameMethods2.Instance.PositionGoahead(allPlayerMark[player - 1], playerName, PlayerData.Name);
        }
    }
    [PunRPC]
    public void CallClearPlate(int who)
    {
        GameMethods2.Instance.ClearPlate(AllPlatePos[who - 1], AllPlateCount[who - 1]);
    }
    [PunRPC]
    public IEnumerator CallClearGuestData(int player, int guest)
    {
        print("Test clear");
        GameObject Player = AllPlayer[player - 1];
        GameObject[] AllGuest = {Player.transform.Find("Guest1").gameObject,
                                Player.transform.Find("Guest2").gameObject,
                                Player.transform.Find("Guest3").gameObject,
                                Player.transform.Find("Guest4").gameObject};
        //StartCoroutine(ClearGuestData(AllGuest[guest - 1]))
        ClearGuestData(AllGuest[guest-1]);
        while (!waitMechanism.getOk())
        {
            yield return 0;
        }
        waitMechanism.SetOkToFalse();
    }
    [PunRPC]
    public void CallAddPlate(int player)
    {
        GameObject addPos = AllPlatePos[player - 1];
        GameObject plateCount = AllPlateCount[player - 1];
        GameMethods2.Instance.AddPlate(addPos, plateCount, Plates);
    }
    [PunRPC]
    public void CallAddMoney(int player, int money)
    {
        
        GameMethods2.Instance.AddMoney(AllPlayerMoney[player - 1], money);
        //string playerName = AllPlayerName[player - 1];
        string playerName = PlayerOrder[player];
        if (PhotonNetwork.NickName.Equals(playerName))
        {
            PlayerData.Money += money;
        }
        
    }
    [PunRPC]
    public void CallAddPublicCharacterMoney(int which)
    {
        GameMethods2.Instance.AddPublicCharacterMoney(AllPublicCharacter[which - 5]);
    }
    [PunRPC]
    public void CallSubPublicCharacterMoney(int which)
    {
        GameMethods2.Instance.SubPublicCharacterMoney(AllPublicCharacter[which - 5]);
    }
    [PunRPC]
    public void CallSetGuestData(int player, int peopleCount)
    {
        ClearGuestData(AllPlayerGuest[player - 1]);
        string playerName = PlayerOrder[turn % PlayerCount + 1];

        if (PhotonNetwork.NickName.Equals(playerName))
        {
            GameMethods2.Instance.UseWelcomerGetNewGuestData(player, PlayerData, GuestCard, AllPlayerGuest);
        }
        else
        {
            for (int i = 0; i < peopleCount; i++)
            {
                GuestCardData2 cardinfo = GuestCard.Dequeue();
                // add info at this
                SetGuestData(AllPlayerGuest[i + (player - 1) * 4], cardinfo);
            }
        }
        
    }
    [PunRPC]
    public void CallAddActionLog_SBB(int which)//steaker, baker, bar
    {
        string role = which == 1 ? "staker" : which == 2 ? "baker" : "bar";
        string item = which == 1 ? "stake" : which == 2 ? "cake" : "juice";
        GameMethods2.Instance.AddActionLog_SBB(role, item);

    }
    [PunRPC]
    public void CallAddActionLog_Chef(string item)
    {
        GameMethods2.Instance.AddActionLog_Chef(item);
    }
    [PunRPC]
    public void CallAddActionLog_Build(string which)
    {
        GameMethods2.Instance.AddActionLog_Build(which);
    }
    [PunRPC]
    public void CallAddActionLog_Cleaner()
    {
        GameMethods2.Instance.AddActionLog_Cleaner();
    }
    [PunRPC]
    public void CallAddActionLog_Welcomer(int guestCapacity)
    {
        GameMethods2.Instance.AddActionLog_Welcomer(guestCapacity);
    }
    //[PunRPC]
    /*public void CallChangeTurnTable()
    {
        GameMethods2.Instance.ChangeTurnTable(PlayerCount, turn, AllTurnTable, AllTurnTableMaterial);
    }*/
    #endregion
    #endregion


    #region GetButtonData
    public void ChangeCharacter(int which)
    {
        
        int characterMoney = 
                GameMethods2.Instance.GetStringValue(
               AllPublicCharacter[which - 5].transform.Find("Money").GetComponent<TextMeshProUGUI>().text);


        //PlayerData.Money += characterMoney;
        int player = turn % PlayerCount + 1;
        photonView.RPC("CallChangeRole", RpcTarget.All, which);
        if (characterMoney != 0)
        {
            photonView.RPC("CallAddMoney", RpcTarget.All, player, characterMoney);
        }
    }

    public void SupportUse(int which, bool self)// position  
    {
        //Debug.Log("Support Use " + which);
        bool moneyEnough = false;
        int player = turn % PlayerCount + 1;

        bool BuildPayOther = false;
        buildUseWho = -1;
        buildPos = -1;
        if (which == player)
        {
            moneyEnough = true;
            buildUseWho = 0;
            buildPos = 0;
        }
        else if (which < 5)
        {
            buildUseWho = 2;
            buildPos = which;
            //get money to other
            if(PlayerData.Money >= 2)
            {
                //PlayerData.Money -= 2;
                BuildPayOther = true;
                photonView.RPC("CallAddMoney", RpcTarget.All, player, -2);
                photonView.RPC("CallAddMoney", RpcTarget.All, which, 2);
                moneyEnough = true;
            }  
        }
        else if (5 <= which && which <= 7)
        {
            buildUseWho = 1;
            buildPos = which;
            if(PlayerData.Money >= 1)
            {
                BuildPayOther = true;
                photonView.RPC("CallAddMoney", RpcTarget.All, player, -1);
                photonView.RPC("CallAddPublicCharacterMoney", RpcTarget.All, which);
                moneyEnough = true;
            } 
        }
        if (moneyEnough)
        {
            
            int abilityCode = CharacterPos[which];
            AudioController.Instance.setAudio(abilityCode);
            if (1 <= abilityCode && abilityCode <= 3)
            {
                //judge ability Code to know which desert will be maked
                //check PlayerData to know the guest data

                ArrayList createPos =
                    GameMethods2.Instance.UseAbilityMakeDeserts(abilityCode, PlayerData, AllPlayer[player - 1]);//123

                var o = new MemoryStream(); //Create something to hold the data
                var bf = new BinaryFormatter(); //Create a formatter
                bf.Serialize(o, createPos); //Save the list
                var data = Convert.ToBase64String(o.GetBuffer()); //Convert the data to a string

                photonView.RPC("CallCreateFood", RpcTarget.All, data, abilityCode);//tip info -> show canvas
                photonView.RPC("CallAddActionLog_SBB", RpcTarget.All, abilityCode);
                //Queue checkGest = (Queue)createPos[0];

                GameMethods2.Instance.CheckCompleted(PlayerData, AllPlayer[player - 1], player, (Queue)createPos[0]); // 0 mean guest 1 mean people
                /*while (!waitMechanism.getOk())
                {
                    yield return 0;
                }*/
                //waitMechanism.SetOkToFalse();
                photonView.RPC("CallSupportUse", RpcTarget.All, which);//tip info -> show canvas
                                                                       //ActionController.Instance.addAction(1);
                int actionCode = self ? 1 : 2;
                ActionController.Instance.addAction(actionCode);
            }
            else if (abilityCode == 4)// chef
            {
                clickObect.enabled = !clickObect.enabled;
                GameMethods2.Instance.SetSelfClick(self);
                ActionController.Instance.LetOtherCannotUse();
                ActionController.Instance.LetSelfCannotUse();
                /*if (self)
                {
                    ActionController.Instance.LetSelfCannotUse();
                }
                else
                {
                    ActionController.Instance.LetOtherCannotUse();
                }*/
                GameMethods2.Instance.LetPeopleCanBeClick(AllPlayer[player - 1], PlayerData);
            }
            else if (abilityCode == 5)
            {
                //ini
                GameMethods2.Instance.SetBuildSelf(false);
                GameMethods2.Instance.SetBuildOtherPlayer(-1);
                GameMethods2.Instance.SetUseBuildPlayer(-1);
                //GameMethods2.Instance.SetBuildPos(-1);
                //set
                GameMethods2.Instance.SetBuildOtherPlayer(which);
                GameMethods2.Instance.SetUseBuildPlayer(player);
                
                if (BuildPayOther)
                {  
                    //set
                    GameMethods2.Instance.SetBuildSelf(false);
                }
                else
                {
                    //set
                    GameMethods2.Instance.SetBuildSelf(true);          
                }
                GameMethods2.Instance.SetSelfClick(self);
                GameMethods2.Instance.SetBuildText(BonuseMoney, TableMoney, PlayerData);
                ButtonController2.Instance.ShowBuildAbilityView();
            }
            else if (abilityCode == 6)
            {
                PlayerData.Dishes = 0;

                photonView.RPC("CallClearPlate", RpcTarget.All, player);
                photonView.RPC("CallAddActionLog_Cleaner", RpcTarget.All);

                int actionCode = self ? 1 : 2;
                ActionController.Instance.addAction(actionCode);
            }
            else if (abilityCode == 7)
            {
                bool can = GameMethods2.Instance.CheckAllGuestCompleted(PlayerData);
                bool positive = PlayerData.Money - PlayerData.Dishes >= 0 ? true : false;
                if (can && positive)
                {
                    int peopleCount = PlayerData.GuestCapacity;
                    photonView.RPC("CallSetGuestData", RpcTarget.All, player, peopleCount);

                    photonView.RPC("CallAddActionLog_Welcomer", RpcTarget.All, PlayerData.GuestCapacity);

                    int dishes = PlayerData.Dishes;
                    photonView.RPC("CallAddMoney", RpcTarget.All, player, -dishes);
                }
                else
                {
                    print("There are some people didn't completed or insufficient balance");
                }
                int actionCode = self ? 1 : 2;
                ActionController.Instance.addAction(actionCode);
            }
        }
        else
        {
            InsufficientBalance.SetActive(true);
            print("No Money !!!!");
           // status.text += "\n" + "just only " +PlayerData.Money;
        }
        
    }

    public bool ChefClick(int guest, int people)
    {
        int player = turn % PlayerCount + 1;
        Queue createPosGuest = new Queue();
        Queue createPosPeople = new Queue();
        createPosGuest.Enqueue(guest);
        createPosPeople.Enqueue(people);
        ArrayList createPos = new ArrayList();
        createPos.Add(createPosGuest);
        createPos.Add(createPosPeople);
        int FoodCode = GameMethods2.Instance.GetChefCreateFood(guest, people, PlayerData);
        if (FoodCode != 0)
        {
            var data = GameMethods2.Instance.ChangeArrayListToString(createPos);
            
            photonView.RPC("CallCreateFood", RpcTarget.All, data, FoodCode);

            string[] foodItem = { "steak", "cake", "juice" };

            photonView.RPC("CallAddActionLog_Chef", RpcTarget.All, foodItem[FoodCode - 1]);
            
            /*GameMethods2.Instance.AddActionLog_Chef(foodItem[FoodCode-1]);
            LogInfoController.Instance.ChangeHeight();*/
            GameMethods2.Instance.CheckCompleted(PlayerData, AllPlayer[player - 1], player, (Queue)createPos[0]);
            return true;
        }
        else
        {
            return false;
        }
    }
    public void FinishChefClick()
    {
        clickObect.enabled = false;
    }

    public void BuildClick(int which) // 1 bonuse 2 table
    {
        //if money enough call builduse
        //bool use = false;
        int cost = GameMethods2.Instance.GetBuildAbilityMoney(BonuseMoney, TableMoney, which);
        if(PlayerData.Money >= cost)
        {
            //PlayerData.Money -= cost;
            if (which == 1)
            {
                PlayerData.MoneyBonus++;
            }
            else
            {
                PlayerData.GuestCapacity++;
            }
            int player = turn % PlayerCount + 1;
            //create new table but money didn't be decrease
            //print("before : " + PlayerData.Money);
            photonView.RPC("CallAddMoney", RpcTarget.All, player, -cost);
            //print("after : " + PlayerData.Money);
            photonView.RPC("CallBuildUse", RpcTarget.All, which);
            string ability = which == 1 ? "Bonuse Level Up" : "Add Guest Capacity";
            photonView.RPC("CallAddActionLog_Build", RpcTarget.All, ability);
        }
        else
        {
            InsufficientBalance.SetActive(true);
            print("Money Not Enough!!, need " + cost);

            if (buildUseWho == 0)// 0 mean self 1 mean public 2 mean other
            {
                SendSelfCanUse();
            }
            else if(buildUseWho == 1 || buildUseWho == 2)
            {
                SendOtherCanUse();
                int player = turn % PlayerCount + 1;
                if (buildUseWho == 2)
                {
                    photonView.RPC("CallAddMoney", RpcTarget.All, player, 2);
                    photonView.RPC("CallAddMoney", RpcTarget.All, buildPos, -2);
                }
                else
                {
                    photonView.RPC("CallAddMoney", RpcTarget.All, player, 1);
                    photonView.RPC("CallSubPublicCharacterMoney", RpcTarget.All, buildPos);
                }
            }
            
        }
    }
    public void CallCancelBuild()
    {
        ButtonController2.Instance.HideBuildAbilityView();
        GameMethods2.Instance.CancelBuildUse();
    }

    public void UsePlayerClick(int who)
    {
        Debug.Log("Click");
        int player = turn % PlayerCount + 1;
        //int abilityCode = CharacterPos[player];
        if (who == player)
        {
            SupportUse(who,true);
        }
        else
        {  
            SupportUse(who, false);
        }
    }

    public void SendOtherCannotUse()
    {
        int player = turn % PlayerCount + 1;
        Support.SetActive(false);
        //Support.SetActive(false);
        GameMethods2.Instance.SetOtherCannotUse(player, AllUsePlayer);
    }
    public void SendSelfCannotUse()
    {
        int player = turn % PlayerCount + 1;
        GameMethods2.Instance.SetSelfCannotUse(player, AllUsePlayer);
    }
    public void SendOtherCanUse()
    {
        int player = turn % PlayerCount + 1;
        Support.SetActive(true);
        GameMethods2.Instance.SetOtherCanUse(player, AllUsePlayer);
    }
    public void SendSelfCanUse()
    {
        int player = turn % PlayerCount + 1;
        GameMethods2.Instance.SetSelfCanUse(player,AllUsePlayer);
    }

    public void ClickCancelToChangeMoney(int player, int money)
    {
        photonView.RPC("CallAddMoney", RpcTarget.All, player, money);
    }

    #endregion

    public void TurnEnd()
    {
        //if chef used then set it to initialization
        if (clickObect.enabled)
        {
            ClickObect.Instance.SetClickTimesToZero();
            clickObect.enabled = false;
        }
        
        GameMethods2.Instance.InitializationAction(ControlButton, AllUsePlayer);
        ControlButton.SetActive(false);
        changeAction = false;
        //GameMethods2.Instance.CheckAllGuestCompleted(PlayerData);
        //GameMethods2.Instance.CheckAllPlateClear(PlayerData);


        photonView.RPC("NextTurn", RpcTarget.MasterClient, PlayerData.Name);
    }

    public void NeedAddPlate()
    {
        //Debug.Log("Add Plate this didn't implement");
        int player = turn % PlayerCount + 1;
        PlayerData.Dishes++;
        photonView.RPC("CallAddPlate", RpcTarget.All, player);
    }
    public void CardCompleted(int player, int guest, int earn)
    {
        int money = 0;
        money += (PlayerData.MoneyBonus + earn);

        photonView.RPC("CallAddMoney", RpcTarget.All, player, money);
        photonView.RPC("CallClearGuestData", RpcTarget.Others, player , guest);
        GameObject Player = AllPlayer[player - 1];
        GameObject[] AllGuest = {Player.transform.Find("Guest1").gameObject,
                                Player.transform.Find("Guest2").gameObject,
                                Player.transform.Find("Guest3").gameObject,
                                Player.transform.Find("Guest4").gameObject};
        ClearGuestData(AllGuest[guest-1]);
    }

    public PlayerData2 GetPlayerData()
    {
        return PlayerData;
    }
    public bool getChangeAction()
    {
        return changeAction;
    }

    public void FinalTurnTrigger()
    {
        int player = turn % PlayerCount + 1;
        Debug.Log("Player = " + player);
        /*if(player == PlayerCount)
        {
            //show GameFinished
            //TurnMessage.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Game Finished";
            print("Game End");
            photonView.RPC("CallGameFinished", RpcTarget.All);
        }
        else
        {
            //show last round
            //TurnMessage.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Final Turn";
            
        }*/
        photonView.RPC("CallShowLastRound", RpcTarget.All);
    }
    public void ProcessFinalData()
    {
        int[] finalOrder = GameMethods2.Instance.ScoreOrder(FinalData);
        string finalData = GameMethods2.Instance.ChangeListStringArrayTypeToString(FinalData);
        //string finalData = GameMethods2.Instance.ChangeArrayListToString(FinalData);
        photonView.RPC("CallShowFinalInfo", RpcTarget.All, finalOrder, finalData);
    }

    public int GetPlayer()
    {
        int player = turn % PlayerCount + 1;
        return player;
    }
    public GameObject[] GetAllUsePlayer()
    {
        return AllUsePlayer;
    }
    public GameObject GetBtSup()
    {
        return Support;
    }

    public void ClickCancelToSubPublicAreaMoney(int which)
    {
        photonView.RPC("CallSubPublicCharacterMoney", RpcTarget.All, which);
    }
    
    public void BeforeAddAction()
    {
        //selfMoney.text += "\nBefore ActionTimes = " + ActionController.Instance.GetAction()+"";
    }
    public void AfterAddAction()
    {
        //selfMoney.text += "\nAfter ActionTimes = " + ActionController.Instance.GetAction();
    }

    public void CallSendRecordData()
    {
        StartCoroutine(GameMethods2.Instance.SendRecordData());
    }
    public void CallCheckStatus()
    {
        ArrayList status = GameMethods2.Instance.guestStatus(PlayerData);
        GameMethods2.Instance.showTipsCard(status, AllCharacterSprite);
    }
    public void CallClearTips()
    {
        GameMethods2.Instance.clearTips();
    }
}
