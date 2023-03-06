using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObect : MonoBehaviour
{
    // Start is called before the first frame update
    static int clickTimes = 0;
    static int guest = 0;
    static int people = 0;

    public static ClickObect Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        clickTimes = 0;
        guest = 0;
        people = 0;
        //Debug.Log("TEST");
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        { // 如果指到 Collider
            if (Input.GetMouseButtonDown(0))
            { // 如果按下滑鼠左鍵
                if (hit.collider.name == "Cube")
                {
                    print("OK");
                }
                if (hit.collider.tag == "Guest1")
                { // 如果該物件
                    guest = 1;
                    
                }
                else if(hit.collider.tag == "Guest2")
                {
                    guest = 2;
                    
                }
                else if(hit.collider.tag == "Guest3")
                {
                    guest = 3;
                    
                }
                else if (hit.collider.tag == "Guest4")
                {
                    guest = 4;
                    
                }
                if(guest != 0)
                {
                    if (hit.collider.name == "people1")
                    {
                        people = 1;
                    }
                    else if (hit.collider.name == "people2")
                    {
                        people = 2;
                    }
                    else if (hit.collider.name == "people3")
                    {
                        people = 3;
                    }
                }
                
                if(guest!=0 && people != 0)
                {
                    print("G" + guest + "P" + people);
                    bool check = GameManager2.Instance.ChefClick(guest, people);
                    if (check)
                    {
                        clickTimes++;
                        if(clickTimes == 2)
                        {
                            clickTimes = 0;
                            bool self = GameMethods2.Instance.GetSelf();
                            int mode = self ? 1 : 2;
                            if (mode == 1)
                            {
                                GameManager2.Instance.SendOtherCanUse();
                                //GameMethods2.Instance.SetOtherCanUse();
                            }
                            else
                            {
                                GameManager2.Instance.SendSelfCanUse();
                            }
                            ActionController.Instance.addAction(mode);
                            GameManager2.Instance.FinishChefClick();
                        }
                    }
                    people = 0;
                    guest = 0;
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            print("Click mouse right");
            bool self = GameMethods2.Instance.GetSelf();
            int mode = self ? 1 : 2;// 1 self 2 other
            if (clickTimes > 0)
            {

                if(mode == 1)
                {
                    GameManager2.Instance.SendOtherCanUse();
                    //GameMethods2.Instance.SetOtherCanUse();
                }
                else
                {
                    GameManager2.Instance.SendSelfCanUse();
                }
                
            }
            else
            {
                GameManager2.Instance.SendOtherCanUse();
                GameManager2.Instance.SendSelfCanUse();
            }
            clickTimes = 0;
            ActionController.Instance.addAction(mode);
            GameManager2.Instance.FinishChefClick();
        }
        
    }
    public void SetClickTimesToZero()
    {
        clickTimes = 0;
    }
}
