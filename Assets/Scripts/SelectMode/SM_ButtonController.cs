using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SM_ButtonController : MonoBehaviour
{
    // Start is called before the first frame update

    /*[SerializeField] Button btOffLine = null;
    [SerializeField] Button btOnline = null;*/

    //[SerializeField] Image showRole;
    [SerializeField] GameObject SettingView = null;
    [SerializeField] GameObject RecordItems = null;
    [SerializeField] GameObject RecordItemsContent = null;
    [SerializeField] GameObject loading = null;
    [SerializeField] GameObject ProfileView = null;
    [SerializeField] GameObject Bt_changeView = null;
    AudioSource audioSource = null;
    //static bool first = true;
    void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
    }
    void Start()
    {
        //print("Test start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayClickAudio()
    {
        audioSource.Play();
    }
    public void Setting()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene();
        //SM_Mode_Static.isOnline = false;
        SettingView.SetActive(!SettingView.activeSelf);
    }
    public void onLine() // 後續補上異步加載
    {
        DontDestroyOnLoad(GameObject.Find("UserData"));
        //DontDestroyOnLoad(GameObject.Find("AudioSetting"));
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
    }
    public void clickTeach()
    {
        DontDestroyOnLoad(GameObject.Find("UserData"));
        //DontDestroyOnLoad(GameObject.Find("AudioSetting"));
        SceneManager.LoadScene("Teach");
    }

    public void exit()
    {
        Application.Quit();
    }
    public void BackToHome()
    {
        //Debug.Log(User.Instance.userData);
        loading.SetActive(true);
        MenuManager.Instance.OpenMenu("Analysis");
        DatabaseHandler.UpdateUserAllData(User.Instance.userData, User.Instance.userData.localId);
        ProfileView.SetActive(false);
        Bt_changeView.SetActive(true);
        loading.SetActive(false);
    }
    public void CallClickProfile()
    {
        print("child count = " + RecordItemsContent.transform.childCount);
        loading.SetActive(true);
        Bt_changeView.SetActive(false);
        StartCoroutine(ClickProfile());
    }
    public IEnumerator ClickProfile()
    {

        /*for (int j = 0; j < foodIndex.Length; j++)
        {
            if (foodIndex[j].transform.childCount > 0)
            {
                Destroy(foodIndex[j].transform.Find("Food").gameObject);
            }
        }*/


        
        DatabaseHandler.GetRecords(User.Instance, User.Instance.userData.localId);
        while (!waitMechanism.getOk())
        {
            yield return 0;
        }
        foreach(Transform child in RecordItemsContent.transform)
        {
            Destroy(child.gameObject);
        }

        /*while (RecordItemsContent.transform.childCount > 0)
        {
            Destroy(RecordItemsContent.transform.Find("recordItem").gameObject);
        }*/
        waitMechanism.SetOkToFalse();
        Record[] records = User.Instance.getRecords();
        //print("len = " + records.Length);
        
        
        for (int i = 0; i < records.Length; i++)
        {
            if (!records[i].date.Equals(""))
            {
                GameObject recordItem = Instantiate(RecordItems, RecordItemsContent.transform);
                recordItem.name = "recordItem";
                recordItem.GetComponent<RecordItemData>().SetUp(
                    records[i].date, records[i].bonuse, records[i].guest, records[i].money,
                    records[i].score, records[i].rank);
            }
        }
        loading.SetActive(false);
        ProfileView.SetActive(true);
        //yield return 1;
    }
    public void CallClickRecord()
    {

    }
    public void ClickSetting()
    {
        SettingView.SetActive(true);
        Bt_changeView.SetActive(false);
    }
    public void ClickSettingBackToHome()
    {
        Bt_changeView.SetActive(true);
        SettingView.SetActive(false);
    }
}
