using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    // Start is called before the first frame update
    static int actionTimes = 0;
    public static ActionController Instance;
    //public int times = 0;
    void Awake()
    {
        Instance = this;
    }
    /*void Start()
    {
        actionTimes = 0;
    }*/

    // Update is called once per frame
    void Update()
    {
        //times = actionTimes;
        //Debug.Log("times = " + times);
        
    }
    public void addAction(int code) // if code == 1 mean use self if code == 2 mean use other
    {
        //Debug.Log("before action = " + actionTimes);
        print("Why main client Add");
        GameManager2.Instance.BeforeAddAction();
        actionTimes++;
        GameManager2.Instance.AfterAddAction();
        //Debug.Log("now action = " + actionTimes);
        if (JudgeActionEnd(code))
        {
            //Debug.Log("test " + actionTimes);
            Debug.Log("Call End");
            GameManager2.Instance.TurnEnd();
        }
    }
    public bool JudgeActionEnd(int code)
    {
        if(actionTimes == 2)
        {
            actionTimes = 0;
            Debug.Log("End");
            return true;
        }
        else
        {
            if(code == 1)
            {
                LetSelfCannotUse();
            }
            else if(code == 2)
            {
                LetOtherCannotUse();
            }
            return false;
        }
    }
    public void SetNotActive()
    {
        actionTimes = 0;
        this.gameObject.SetActive(false);
    }
    public void LetOtherCannotUse()
    {
        GameManager2.Instance.SendOtherCannotUse();
    }
    public void LetSelfCannotUse()
    {
        GameManager2.Instance.SendSelfCannotUse();
    }
    public void SetTimesToZero()
    {
        actionTimes = 0;
    }
    public int GetAction()
    {
        return actionTimes;
    }
}
