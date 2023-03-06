using TMPro;
using UnityEngine;

public class BuildViewSetting : MonoBehaviour
{
    [SerializeField] GameObject BonuseMoney = null;
    [SerializeField] GameObject TableMoney = null;

    public static BuildViewSetting Instance;
    
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
    public void SetText()
    {
        Debug.Log("SetBuildViewData");
        PlayerData2 playerData = GameManager2.Instance.GetPlayerData();
        int playerBonuse = playerData.MoneyBonus;
        int playerTable = playerData.GuestCapacity;
        int tableLevelUp = playerBonuse + 5;
        int bonuseLevelUp = playerTable == 4 ? playerTable + 3 : playerTable + 1;
        BonuseMoney.GetComponent<TextMeshProUGUI>().text = bonuseLevelUp + "";
        TableMoney.GetComponent<TextMeshProUGUI>().text = tableLevelUp + "";
        
        //BonuseMoney.transform.GetComponent<TextMeshPro>().text = 
    }
}
