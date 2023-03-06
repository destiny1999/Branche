using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;

public class GameMethods2 : MonoBehaviour
{
    public static GameMethods2 Instance;
    static bool chefSelf = false;

    static int useBuildPlayer = -1;
    static int buildOtherPlayer = -1;
    static int BuildPos = -1;
    static bool buildself = false;
    //[SerializeField] GameObject welcomeTestText = null;
    //[SerializeField] GameObject welcomeTestTextParent = null;
    //[SerializeField] GameObject welcomeTestContent = null;
    [SerializeField] RectTransform TipsCardView = null;
    [SerializeField] GameObject TipsCard = null;

    void Awake()
    {
        Instance = this;
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPublicCharacterMoney(GameObject target)
    {
        int money = GetStringValue(
            target.transform.Find("Money").GetComponent<TextMeshProUGUI>().text);
        money++;
        target.transform.Find("Money").GetComponent<TextMeshProUGUI>().text = money+"";
    }
    public void SubPublicCharacterMoney(GameObject target)
    {
        int money = GetStringValue(
            target.transform.Find("Money").GetComponent<TextMeshProUGUI>().text);
        money--;
        if(money == 0)
        {
            target.transform.Find("Money").GetComponent<TextMeshProUGUI>().text = "";
        }
        else
        {
            target.transform.Find("Money").GetComponent<TextMeshProUGUI>().text = money + "";
        }
        
    }
    public void ChangePublicAreaRoleSprite(GameObject target, Sprite sprite)
    {
        target.transform.Find("Money").GetComponent<TextMeshProUGUI>().text = "";
        target.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
    }

    public void ChangeRole(GameObject player, GameObject Character)
    {
        Transform characterPos = player.transform.Find("CharacterPos").transform;
        GameObject character = Instantiate(Character, characterPos.position,
            characterPos.rotation);
        character.transform.SetParent(player.transform);
        character.name = "Character";
        //character.transform.localScale = new Vector3(250, 250, 250);
    }
    public ArrayList UseAbilityMakeDeserts(int abilityCode, PlayerData2 playerData, GameObject Player)
    {
        //check the sprite is null or not
        //if the sprite is null then check next sprite
        //if the sprite is not null then check queue
        //if queue's data is same as target let the sprite become null and create object
        //if queue's data different from target then check next queue
        //if the sprite all null mean this queue completed

        Queue createPosGuest = new Queue();
        Queue createPosPeople = new Queue();
        ArrayList createPos = new ArrayList();

        int guest = 1;
        //int cardindex = 0;
        //ArrayList completedCardindex = new ArrayList();
        //if check the target then use RPC, it mean just player should check other just do
        foreach(GuestCardData2 card in playerData.CardList) // get card
        {
            int people = 1;
            foreach(Queue itemList in card.ItemList)// get each items like 2 1 3
            {
                if (itemList.Count > 0)// if not null
                {
                    if ((int)itemList.Peek() == abilityCode)// if first == target
                    {
                        createPosGuest.Enqueue(guest); // the guest(1 2 3 or 4 ) should be add
                        createPosPeople.Enqueue(people); // the people (1 2 or 3) should be add
                        int takeAway = (int)itemList.Dequeue(); // take away the target from list
                        if(itemList.Count == 0) // if empty
                        {
                            int CP = card.CompletedPeople; // get card completed num to find empty or not
                            CP++; // becuase new card copmleted so add 1
                            card.CompletedPeople = CP; // change record
                            GameManager2.Instance.NeedAddPlate(); // check each list so just add 1
                        }
                        //Debug.Log("Throw " + takeAway);
                    }
                }
                people++;
            }
            guest++;
        }
        createPos.Add(createPosGuest);
        createPos.Add(createPosPeople);
        return createPos;
    }
    public void CreateFood(GameObject Player, ArrayList CreatePos, GameObject target, int abilityCode, Sprite UIMask)
    {
        //Debug.Log("Use CreateFood");
        // item        pos          rotate       scale
        // cake    (0,0,-0.5)      (0,180,0)       3
        // juice    (0,0,1.1)     (90,0,-180)     1.1
        // steak    (0,0,0.5)     (90,0,-180)     0.7
        //Instantiate target at Player's CreatePos(should check where just know guest and people)
        //Debug.Log("Create" + target.name);
        Queue createPosGuest = (Queue)CreatePos[0];
        Queue createPosPeople = (Queue)CreatePos[1];
        GameObject[] Guest = { Player.transform.Find("Guest1").gameObject,
                                Player.transform.Find("Guest2").gameObject,
                                Player.transform.Find("Guest3").gameObject,
                                Player.transform.Find("Guest4").gameObject};

        while (createPosGuest.Count > 0)
        {
            int guest = (int)createPosGuest.Dequeue(); //

            int people = (int)createPosPeople.Dequeue();
            GameObject[] People = { Guest[guest - 1].transform.Find("items").Find("1").gameObject,
                            Guest[guest - 1].transform.Find("items").Find("2").gameObject,
                            Guest[guest - 1].transform.Find("items").Find("3").gameObject};

            GameObject targetPeople = People[people - 1];
            GameObject[] foods = {targetPeople.transform.Find("1").gameObject,
                            targetPeople.transform.Find("2").gameObject,
                            targetPeople.transform.Find("3").gameObject,
                            targetPeople.transform.Find("4").gameObject};

            for(int i = 0; i<4; i++)
            {
                if(foods[i].GetComponent<SpriteRenderer>().sprite != UIMask)
                {
                    //Debug.Log("Create " + abilityCode);

                    Transform foodPos = foods[i].transform;
                    GameObject food = Instantiate(target, foodPos.position, foodPos.rotation);
                    food.transform.SetParent(foods[i].transform);
                    food.name = "Food";
                    if (abilityCode == 1)// steak
                    {
                        food.transform.localScale = new Vector3(0.07f,0.07f,0.07f);
                        food.transform.localPosition = new Vector3(0, 0, 0.5f);
                        food.transform.Rotate(270, 0, 0);
                    }
                    else if(abilityCode == 2)// cake
                    {
                        food.transform.localScale = new Vector3(0.09f, 0.09f, 0.09f);
                        food.transform.localPosition = new Vector3(0, 0, -0.5f);
                        food.transform.Rotate(270, 0, 0);
                    }
                    else if(abilityCode == 3) // juice
                    {
                        //food.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
                        food.transform.localPosition = new Vector3(0, 0, 1.1f);
                        food.transform.Rotate(90, 0, -180);
                        //food.transform.localRotation = Quaternion.Euler(90, 0, -180,0);
                        //Quaternion.Euler(0, 0, 0);
                    }
                    foods[i].GetComponent<SpriteRenderer>().sprite = UIMask;
                    break;
                }
            }

        }

    }
    public void CheckCompleted(PlayerData2 playerData, GameObject Player, int player, Queue GuestQueue)
    {
        
        Debug.Log("Check");
        GameObject[] AllGuest = {Player.transform.Find("Guest1").gameObject, Player.transform.Find("Guest2").gameObject,
                                Player.transform.Find("Guest3").gameObject, Player.transform.Find("Guest4").gameObject};
        int cardnum = 1;
        foreach (GuestCardData2 card in playerData.CardList)// check each card mean each guest
        {
            Debug.Log(card.CardName);
            if(GuestQueue.Count>0 && cardnum == (int)GuestQueue.Peek())
            {
                Debug.Log("Completed is " + cardnum);
                if (card.CompletedPeople == card.People) // mean all people completed
                {
                    GameManager2.Instance.CardCompleted(player, cardnum, card.Money);
                }
                GuestQueue.Dequeue();
            }
            cardnum++;
        }
    }

    public void LetPeopleCanBeClick(GameObject Player, PlayerData2 PlayerData)
    {
        GameObject[] AllGuest = {Player.transform.Find("Guest1").gameObject,
                                Player.transform.Find("Guest2").gameObject,
                                Player.transform.Find("Guest3").gameObject,
                                Player.transform.Find("Guest4").gameObject};

        List<GuestCardData2> guest = PlayerData.CardList;

        for(int i = 0; i<guest.Count; i++)
        {
            if (AllGuest[i].activeSelf)
            {
                //Debug.Log(AllGuest[i].name + " is active.");
                GameObject[] AllPeople = {AllGuest[i].transform.Find("people1").gameObject,
                                        AllGuest[i].transform.Find("people2").gameObject,
                                        AllGuest[i].transform.Find("people3").gameObject};

                List<Queue> ItemList = guest[i].ItemList;

                for (int j = 0; j<guest[i].People; j++)
                {
                    Queue foods = ItemList[j];
                    if (foods.Count > 0)
                    {
                        AllPeople[j].GetComponent<CapsuleCollider>().enabled = true;
                        //AllPeople[j].GetComponent<AdjustCollider>().enabled = true;
                    }
                }
            }
        }

    }

    public void BonuseLevelUp(GameObject target, string useName, string selfName)
    {
        int bonuse = 0;
        if (target.GetComponent<TextMeshProUGUI>().text.Equals(""))
        {
            bonuse = 1;
        }
        else
        {
            bonuse = int.Parse(target.GetComponent<TextMeshProUGUI>().text)+1;
        }
        //int bonuse = int.Parse(target.GetComponent<TextMeshProUGUI>().text);
        target.GetComponent<TextMeshProUGUI>().text = bonuse + "";
        if (useName.Equals(selfName))
        {
            int actioncode = GetSelf() ? 1 : 2;
            ActionController.Instance.addAction(actioncode);
        }
        /*int actioncode = GetSelf() ? 1 : 2;
        ActionController.Instance.addAction(actioncode);*/
    }
    public void PositionGoahead(GameObject target, string useName, string selfName)
    {
        float Oldx = target.GetComponent<RectTransform>().anchoredPosition.x;
        float Oldy = target.GetComponent<RectTransform>().anchoredPosition.y;

        target.GetComponent<RectTransform>().anchoredPosition = new Vector2(Oldx + 80, Oldy);
        /*Oldx += 80;
        Oldx += 80;*/
        if (Oldx + 80 == 286)
        {
            //print("Game Finished");
            GameManager2.Instance.FinalTurnTrigger();
        }
        /*else
        {
            print("TEST final trigger");
            GameManager2.Instance.FinalTurnTrigger();
        }*/
        if (useName.Equals(selfName))
        {
            int actioncode = GetSelf() ? 1 : 2;
            ActionController.Instance.addAction(actioncode);
        }
        
    }
    public void CreateTable(GameObject player)
    {
        if (player.transform.Find("Guest3").gameObject.activeSelf)
        {
            player.transform.Find("Guest4").gameObject.SetActive(true);
            GameManager2.Instance.ClearGuestData(player.transform.Find("Guest4").gameObject);
        }
        else
        {
            player.transform.Find("Guest3").gameObject.SetActive(true);
            GameManager2.Instance.ClearGuestData(player.transform.Find("Guest3").gameObject);
        }
    }

    public void ClearPlate(GameObject PlatePos, GameObject PlateCount)
    {
        PlateCount.GetComponent<TextMeshPro>().text = "";
        if (PlatePos.transform.childCount > 0)
        {
            Destroy(PlatePos.transform.Find("Plates").gameObject);
        }
        
    }
    public void AddPlate(GameObject PlatePos, GameObject PlateCount, GameObject Plates)
    {
        if(PlatePos.transform.childCount == 0)
        {
            GameObject plates = Instantiate(Plates, PlatePos.transform.position, PlatePos.transform.rotation);
            plates.name = "Plates";
            plates.transform.SetParent(PlatePos.transform);
        }
        string plateCountText = PlateCount.GetComponent<TextMeshPro>().text;
        int plateCount = 0;
        if (plateCountText.Equals(""))
        {
            plateCount = 1;
        }
        else
        {
            plateCount = int.Parse(plateCountText) + 1;
        }
        PlateCount.GetComponent<TextMeshPro>().text = plateCount + "";
    }

    public void AddMoney(GameObject playerMoney, int add)
    {
        //Debug.Log("add = " + add);
        int money = GetStringValue(playerMoney.GetComponent<TextMeshProUGUI>().text);
        //Debug.Log("money = " + money);
        money += add;
        //Debug.Log("After add money = " + money);
        playerMoney.GetComponent<TextMeshProUGUI>().text = money + "";
    }

    public void SetBuildText(GameObject BonuseMoney, GameObject TableMoney, PlayerData2 playerData)
    {
        Debug.Log("SetBuildViewData");
        
        int playerBonuse = playerData.MoneyBonus;
        int playerTable = playerData.GuestCapacity;
        int tableLevelUp = playerBonuse + 5;
        int bonuseLevelUp = playerTable == 4 ? playerTable + 3 : playerTable + 1;
        BonuseMoney.GetComponent<TextMeshProUGUI>().text = bonuseLevelUp + "";
        TableMoney.GetComponent<TextMeshProUGUI>().text = tableLevelUp + "";

        //BonuseMoney.transform.GetComponent<TextMeshPro>().text = 
    }
    public int GetBuildAbilityMoney(GameObject BonuseMoney, GameObject TableMoney, int which)
    {
        int bonuseMoney = int.Parse(BonuseMoney.GetComponent<TextMeshProUGUI>().text);
        int tableMoney = int.Parse(TableMoney.GetComponent<TextMeshProUGUI>().text);
        return which == 1 ? bonuseMoney : tableMoney;
    }

    #region ChangeDataType
    public string ChangeArrayListToString(ArrayList _data)
    {
        var o = new MemoryStream(); //Create something to hold the data
        var bf = new BinaryFormatter(); //Create a formatter

        bf.Serialize(o, _data); //Save the list

        var data = Convert.ToBase64String(o.GetBuffer()); //Convert the data to a string

        return data;
    }

    public string ChangeListStringArrayTypeToString(List<string[]> _data)
    {
        var o = new MemoryStream(); //Create something to hold the data
        var bf = new BinaryFormatter(); //Create a formatter

        bf.Serialize(o, _data); //Save the list

        var data = Convert.ToBase64String(o.GetBuffer()); //Convert the data to a string

        return data;
    }
    public int GetStringValue(string text)
    {
        int res = 0;
        if (!text.Equals(""))
        {
            res = int.Parse(text);
        }
        return res;
    }
    #endregion

    public int GetChefCreateFood(int guest, int people, PlayerData2 PlayerData)
    {
        int code = 0;
        List<GuestCardData2> card = PlayerData.CardList;
        Queue food = card[guest-1].ItemList[people-1];
        if (food.Count > 0)
        {
            code = (int)food.Dequeue();
            //Debug.Log("TakeAway By Chef and the count is " + food.Count);
            if(food.Count == 0)
            {
                int CP = card[guest - 1].CompletedPeople;
                CP++;
                card[guest - 1].CompletedPeople = CP;
                //Debug.Log("Test add or not");
                GameManager2.Instance.NeedAddPlate();
            }
        }
        return code;
    }
    public void SetSelfClick(bool self)
    {
        chefSelf = self;
    }
    public bool GetSelf()
    {
        return chefSelf;
    }
    public void CheckAllPlateClear(PlayerData2 playerData)
    {
        //List<GuestCardData2> cardList = playerData.CardList;
        
    }
    public bool CheckAllGuestCompleted(PlayerData2 playerData)
    {
        bool completed = true;
        List<GuestCardData2> cardList = playerData.CardList;
        foreach(GuestCardData2 card in cardList)
        {
            if(card.CompletedPeople != card.People)
            {
                completed = false;
                break;
            }
        }
        return completed;
    }
    public void UseWelcomerGetNewGuestData(int player, PlayerData2 playerData, Queue<GuestCardData2> GuestCard, GameObject[] AllPlayerGuest)
    {
        playerData.CardList = new List<GuestCardData2>();
        for(int i = 0; i<playerData.GuestCapacity ; i++)
        {
            if (GuestCard.Count > 0)
            {
                GuestCardData2 cardInfo = GuestCard.Dequeue();
                SetWelcomeTestText(cardInfo);
                playerData.CardList.Add(cardInfo);
                GameManager2.Instance.SetGuestData(AllPlayerGuest[i + (player - 1) * 4], cardInfo);
            }
            else
            {
                GameManager2.Instance.FinalTurnTrigger();
                break;
            }
            if (GuestCard.Count == 0)
            {
                GameManager2.Instance.FinalTurnTrigger();
                break;
            }
        }
    }
    public void SetWelcomeTestText(GuestCardData2 cardInfo)
    {
        string itemString = "{";
        List<Queue> ItemList = cardInfo.ItemList;
        /*welcomeTestTextParent.GetComponent<RectTransform>().sizeDelta = new Vector2(
            welcomeTestTextParent.GetComponent<RectTransform>().sizeDelta.x,
            welcomeTestTextParent.GetComponent<RectTransform>().sizeDelta.y+255);*/
        for (int i = 0; i<cardInfo.People; i++)
        {
            Queue foods = new Queue(ItemList[i]);
            while (foods.Count > 0)
            {
                itemString += foods.Dequeue() + " ";
            }
            
            //ItemList[i] Queue foods = ItemList[i];
        }
        itemString += "}";

        for (int i = 0; i<cardInfo.ItemList.Count; i++)
        {
            Queue foods = ItemList[i];
            itemString += " " + foods.Peek();
        }
        //GameObject info = Instantiate(welcomeTestText, welcomeTestTextParent.transform);
        /*info.GetComponent<TextMeshProUGUI>().text = "\ncardName: "+cardInfo.CardName + "\n"+
                                                    "cardPeople: " + cardInfo.People + "\n"+
                                                    "cardMoney: " + cardInfo.Money + "\n"+
                                                    "cardItem: " + itemString;*/

    }
    #region Turn Methods
    public void InitializationAction(GameObject ControlButton, GameObject[] AllUsePlayer)
    {
        ActionController.Instance.SetNotActive();
        //ControlButton.SetActive(false);
        ControlButton.transform.Find("Call").gameObject.SetActive(true);
        ControlButton.transform.Find("PublicCharacter").gameObject.SetActive(false);
        ControlButton.transform.Find("SupportCharacter").gameObject.SetActive(false);
        ControlButton.transform.Find("Support").gameObject.SetActive(false);
        ControlButton.transform.Find("UsePlayer").gameObject.SetActive(false);
        for (int i = 0; i<AllUsePlayer.Length; i++)
        {
            AllUsePlayer[i].SetActive(true);
        }
    }
    public void SetOtherCannotUse(int player, GameObject[] AllUsePlayer)
    {
        for(int i = 0; i<AllUsePlayer.Length; i++)
        {
            if (i != player - 1)
            {
                AllUsePlayer[i].SetActive(false);
            }
        }
        /*for(int i = 0; i<AllSupportButton.Length; i++)
        {
            AllSupportButton[i].SetActive(false);
        }*/
    }
    public void SetOtherCanUse(int player, GameObject[] AllUsePlayer)
    {
        for (int i = 0; i < AllUsePlayer.Length; i++)
        {
            if (i != player - 1)
            {
                AllUsePlayer[i].SetActive(true);
            }
        }
        /*for(int i = 0; i<AllSupportButton.Length; i++)
        {
            AllSupportButton[i].SetActive(false);
        }*/
    }
    public void SetSelfCannotUse(int player, GameObject[] AllUsePlayer)
    {
        for (int i = 0; i < AllUsePlayer.Length; i++)
        {
            if (i == player - 1)
            {
                AllUsePlayer[i].SetActive(false);
            }
        }
        
    }
    public void SetSelfCanUse(int player, GameObject[] AllUsePlayer)
    {
        for (int i = 0; i < AllUsePlayer.Length; i++)
        {
            if (i == player - 1)
            {
                AllUsePlayer[i].SetActive(true);
            }
        }

    }
    public void SetAllPlayerAbilityCanUse(GameObject[] AllUsePlayer)
    {
        for(int i = 0; i<AllUsePlayer.Length; i++)
        {
            AllUsePlayer[i].SetActive(true);
        }
    }
    #endregion

    public void GameFinished()
    {

    }
    public int[] ScoreOrder(List<string[]> finalData)
    {
        int[] order = new int[finalData.Count];
        int[] score = new int[finalData.Count];

        /*
        finalInfo[0] = name;
        finalInfo[1] = money+"";
        finalInfo[2] = plates+"";
        finalInfo[3] = bonuse+"";
        finalInfo[4] = score+"";*/
        
        int index = 0;
        foreach(string[] data in finalData)
        {
            score[index] = int.Parse(data[4]);
            order[index] = index + 1;
            index++;
        }
        for(int i = 0; i<score.Length; i++)
        {
            for(int j = 0; j<score.Length-1; j++)
            {
                if (score[j] < score[j + 1])
                {
                    int temp = score[j];
                    score[j] = score[j + 1];
                    score[j + 1] = temp;
                    temp = order[j];
                    order[j] = order[j + 1];
                    order[j + 1] = temp;
                }
                else if(score[j] == score[j + 1])
                {
                    string[] data_j = finalData[j];
                    string[] data_jj = finalData[j + 1];
                    if(int.Parse(data_j[1]) < int.Parse(data_jj[1]))
                    {
                        int temp = score[j];
                        score[j] = score[j + 1];
                        score[j + 1] = temp;
                        temp = order[j];
                        order[j] = order[j + 1];
                        order[j + 1] = temp;
                    }
                }
            }
        }

        return order;
    }
    public void ShowFinalInfo(GameObject[] Order, List<string[]> data, int[] order)
    {


        string selfName = User.Instance.userData.name;


        for (int i = 0; i < order.Length; i++)
        {
            int index = order[i] - 1;
            GameObject[] info = {Order[i].transform.Find("Name").gameObject,
                                Order[i].transform.Find("Money").gameObject,
                                Order[i].transform.Find("Plates").gameObject,
                                Order[i].transform.Find("Bonuse").gameObject,
                                Order[i].transform.Find("Score").gameObject};
            
            string[] playerInfo = data[index];

            if (playerInfo[0].Equals(selfName))
            {
                Record[] record = User.Instance.getRecords();
                record[0].rank = i + 1;
                User.Instance.setRecords(record);
                GameManager2.Instance.CallSendRecordData();
            }

            for(int j = 0; j<info.Length; j++)
            {

                info[j].GetComponent<TextMeshProUGUI>().text = playerInfo[j];

            }
        }
    }

    public void ChangeTurnTable(int playerCount, int turn, GameObject[] allTurnTable, Material[] allTurnTableMaterial )
    {
        
        int toLight = turn % playerCount + 1; // 1 2 3 4 1 2 3 4
        int toDark = (turn+(playerCount - 1)) % playerCount + 1; // 4 1 2 3 4 1 2 3
        allTurnTable[toDark - 1].GetComponent<MeshRenderer>().material =
            allTurnTableMaterial[0];
        allTurnTable[toLight - 1].GetComponent<MeshRenderer>().material =
            allTurnTableMaterial[toLight];  
    }
    public void CancelBuildUse() //
    {
        bool self = buildself;
        int player = GameManager2.Instance.GetPlayer();
        int pos = buildOtherPlayer;
        Debug.Log("After Status = " + buildself);
        GameObject[] allUsePlayer = GameManager2.Instance.GetAllUsePlayer();
        if (self)
        {
            SetSelfCanUse(player, allUsePlayer);
            Debug.Log("Add money Player = " + player);
            //GameManager2.Instance.ClickCancelToChangeMoney(useBuildPlayer, 2);
        }
        else//publid and otherplayer
        {
            GameManager2.Instance.GetBtSup().SetActive(true);
            SetOtherCanUse(player, allUsePlayer);
            if (pos >= 5)
            {
                GameManager2.Instance.ClickCancelToChangeMoney(useBuildPlayer, 1);
                GameManager2.Instance.ClickCancelToSubPublicAreaMoney(pos);
            }
            else
            {
                GameManager2.Instance.ClickCancelToChangeMoney(useBuildPlayer, 2);
                GameManager2.Instance.ClickCancelToChangeMoney(buildOtherPlayer, -2);
            } 
        }

    }
    public void SetUseBuildPlayer(int player)
    {
        useBuildPlayer = player;
        Debug.Log("Set player = " + player);
    }
    public void SetBuildOtherPlayer(int which)
    {
        buildOtherPlayer = which;
    }
    public void SetBuildSelf(bool status)
    {
        buildself = status;
        Debug.Log("Status = " + buildself);
    }
    public void SetBuildPos(int pos)
    {
        BuildPos = pos;
    }

    public void AddChangeRoleInfo(string playerName, string oldRole, string newRole)
    {
        string newS = $"{playerName} change Role from " +
            $"{oldRole} to {newRole}\n";
        int logLine = 1;
        if(newS.Length> 33)// it mean over one line
        {
            logLine += newS.Length / 33;
        }

        LogInfoController.Instance.getLogInfo().text +=$"{playerName} change Role from " +
            $"{oldRole} to {newRole}\n";
        
        //print("call ChangeHeight");
        //LogInfoController.Instance.ChangeHeight();
        StartCoroutine(LogInfoController.Instance.ChangeHeight(logLine));
    }
    public void AddActionLog_SBB(string role, string item)
    {
        string newS = $"\tuse {role} to make {item}\n";
        int logLine = 1;
        if (newS.Length > 33)// it mean over one line
        {
            logLine += newS.Length / 33;
        }


        LogInfoController.Instance.getLogInfo().text += $"\tuse {role} to make {item}\n";
        StartCoroutine(LogInfoController.Instance.ChangeHeight(logLine));
    }
    public void AddActionLog_Chef(string item)
    {
        string newS = $"\tuse chef to make {item}\n";
        int logLine = 1;
        if (newS.Length > 33)// it mean over one line
        {
            logLine += newS.Length / 33;
        }

        LogInfoController.Instance.getLogInfo().text += $"\tuse chef to make {item}\n";
        StartCoroutine(LogInfoController.Instance.ChangeHeight(logLine));
    }
    public void AddActionLog_Build(string ability)
    {
        string newS = $"\tuse build to {ability}\n";
        int logLine = 1;
        if (newS.Length > 33)// it mean over one line
        {
            logLine += newS.Length / 33;
        }

        LogInfoController.Instance.getLogInfo().text += $"\tuse build to {ability}\n";
        StartCoroutine(LogInfoController.Instance.ChangeHeight(logLine));
    }
    public void AddActionLog_Cleaner()
    {
        string newS = $"\tuse cleaner to clean all plates\n";
        int logLine = 1;
        if (newS.Length > 33)// it mean over one line
        {
            logLine += newS.Length / 33;
        }

        LogInfoController.Instance.getLogInfo().text += $"\tuse cleaner to clean all plates\n";
        StartCoroutine(LogInfoController.Instance.ChangeHeight(logLine));
    }
    public void AddActionLog_Welcomer(int guestCapacity)
    {
        string newS = $"\tuse Receptionist to Recep {guestCapacity} guest\n";
        int logLine = 1;
        if (newS.Length > 33)// it mean over one line
        {
            logLine += newS.Length / 33;
        }

        LogInfoController.Instance.getLogInfo().text += $"\tuse Receptionist to Recep {guestCapacity} guest\n";
        StartCoroutine(LogInfoController.Instance.ChangeHeight(logLine));
    }
    public IEnumerator SaveResultData(UserData user)
    {
        bool completed = false;
        DatabaseHandler.UpdateUserAllData(user, user.localId);
        while (!waitMechanism.getOk())
        {
            yield return 0;
        }
        waitMechanism.SetOkToFalse();
        /*User u = GameObject.Find("UserData").GetComponent<User>();
        DatabaseHandler.UpdateGameStatusRecords(u.getGameStatusRecords(), user.localId);
        while (!completed)
        {
            yield return 0;
        }*/
        Debug.Log("End game Save success");
    }
    public IEnumerator SendRecordData()
    {
        DatabaseHandler.UpdateRecord();
        while (!waitMechanism.getOk())
        {
            //print("wait update final data. . .");
            yield return 0;
        }
        waitMechanism.SetOkToFalse();

        DatabaseHandler.UpdateGameStatusRecords();
        while (!waitMechanism.getOk())
        {
            yield return 0;
        }
        waitMechanism.SetOkToFalse();
    }

    public ArrayList guestStatus(PlayerData2 playerData)
    {
        ArrayList shouldAbility = new ArrayList();
        /*if (playerData.Dishes > 0)
        {
            shouldAbility.Add(6);
        }*/
        int steak = 0;
        int cake = 0;
        int juice = 0;
        foreach(GuestCardData2 card in playerData.CardList)
        {
            foreach(Queue items in card.ItemList)
            {
                if ((int)items.Peek() == 1) steak++;
                else if ((int)items.Peek() == 2) cake++;
                else if ((int)items.Peek() == 3) juice++;
            }
        }
        if(steak == 0 && cake == 0 && juice == 0)
        {
            if(playerData.Dishes > 0)
            {
                shouldAbility.Add(6);
            }
            int playerBonuse = playerData.MoneyBonus;
            int playerTable = playerData.GuestCapacity;
            int tableLevelUp = playerBonuse + 5;
            int bonuseLevelUp = playerTable == 4 ? playerTable + 3 : playerTable + 1;
            if(playerData.Money>=tableLevelUp || playerData.Money >= bonuseLevelUp)
            {
                shouldAbility.Add(5);
            }
            shouldAbility.Add(7);
        }
        else
        {
            if(steak>cake && steak > juice)
            {
                shouldAbility.Add(1);
            }
            else if(cake > steak && cake > juice)
            {
                shouldAbility.Add(2);
            }
            else if(juice > cake && juice > steak)
            {
                shouldAbility.Add(3);
            }
            else
            {
                shouldAbility.Add(4);
            }
        }
        return shouldAbility;
    }
    public void showTipsCard(ArrayList status, Sprite[] allCharacterSprite)
    {
        int width = 250 * status.Count;
        TipsCardView.sizeDelta = new Vector2(width, 234);
        TipsCardView.anchoredPosition = new Vector2(-width / 2, 250.2f);
        foreach(int code in status)
        {
            GameObject tipsCard = GameObject.Instantiate(TipsCard,TipsCardView);
            tipsCard.GetComponent<UnityEngine.UI.Image>().sprite = allCharacterSprite[code - 1];
        }
    }
    public void clearTips()
    {
        foreach (Transform tips in TipsCardView.transform)
        {
            GameObject.Destroy(tips.gameObject);
        }
    }
}
