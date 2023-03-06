using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatingController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject simpleGuest = null;
    [SerializeField] GameObject Guest_up = null;
    [SerializeField] GameObject Guest_down = null;

    [SerializeField] GameObject simplePerson = null;
    [SerializeField] GameObject simpleItem = null;

    [SerializeField] float guestValue = 1;
    [SerializeField] float peopleValue = 1;
    [SerializeField] float itemValue = 1;
    [SerializeField] float itemSelection = -1;

    [SerializeField] GameObject SettingPanel = null;
    [SerializeField] GameObject OperatArea = null;

    static bool chefClick = false;
    static int chefTimes = 0;
    static bool action = false;

    public static OperatingController Instance = null;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addGuest(float value)
    {
        guestValue = value;
        GameObject guest = Instantiate(simpleGuest);
        if (value < 3)
        {
            guest.transform.SetParent(Guest_up.transform);
        }
        else
        {
            guest.transform.SetParent(Guest_down.transform);
        }
        guest.transform.localScale = new Vector3(1, 1, 1);
        guest.name = $"people{value}";
        if(peopleValue != 1)
        {
            adjustPeople(guest);
        }
        if(itemValue != 1)
        {
            adjustItem(guest.transform.Find("People").Find("SimplePerson1").gameObject);
        }
    }
    public void subGuest(float value)
    {
        guestValue = value;
        if(value == 3)
        {
            Destroy(Guest_down.transform.Find("people4").gameObject);
        }
        else if(value == 2)
        {
            Destroy(Guest_down.transform.Find("people3").gameObject);
        }
        else if (value == 1)
        {
            Destroy(Guest_up.transform.Find("people2").gameObject);
        }
    }
    public void adjustPeople(GameObject guest)
    {
        for(int i = 2; i <= peopleValue; i++)
        {
            GameObject person = Instantiate(simplePerson);
            person.name = $"SimplePerson{i}";
            person.transform.SetParent(guest.transform.Find("People"));
            person.transform.localScale = new Vector3(1, 1, 1);
            if (itemValue != 1)
            {
                adjustItem(person);
            }
        }
        
    }

    public void addPeople(float value)
    {
        peopleValue = value;
        for(int i = 1; i<=guestValue; i++)
        {
            GameObject person = Instantiate(simplePerson);
            person.name = $"SimplePerson{peopleValue}";
            if (i < 3)
            {
                person.transform.SetParent(
                    Guest_up.transform.Find($"people{i}").transform.Find("People").transform);
            }
            else
            {
                person.transform.SetParent(
                    Guest_down.transform.Find($"people{i}").transform.Find("People").transform);
            }
            if(itemValue!= 1)
            {
                adjustItem(person);
            }
            person.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void subPeople(float value)
    {
        peopleValue = value;
        for (int i = 1; i <= guestValue; i++)
        {
            if(i < 3)
            {
                Destroy(Guest_up.transform.Find($"people{i}").transform.Find("People")
                    .transform.Find($"SimplePerson{value + 1}").gameObject);
            }
            else
            {
                Destroy(Guest_down.transform.Find($"people{i}").transform.Find("People")
                    .transform.Find($"SimplePerson{value + 1}").gameObject);
            }
        }
    }

    public void adjustItem(GameObject person)
    {
        for(int i = 2; i<= itemValue; i++)
        {
            GameObject item = Instantiate(simpleItem);
            item.name = $"SimpleItem{i}";
            item.transform.SetParent(person.transform);
            item.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void addItem(float value)
    {
        itemValue = value;
        for(int i = 1; i<=guestValue; i++)
        {
            for(int j = 1; j<=peopleValue; j++)
            {
                GameObject item = Instantiate(simpleItem);
                item.name = $"SimpleItem{itemValue}";

                if (i < 3)
                {
                    item.transform.SetParent(
                        Guest_up.transform.Find($"people{i}").transform.Find("People").transform.
                            Find($"SimplePerson{j}").transform);
                }
                else
                {
                    item.transform.SetParent(
                        Guest_down.transform.Find($"people{i}").transform.Find("People").transform.
                            Find($"SimplePerson{j}").transform);
                }
                item.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
    public void subItem(float value)
    {
        itemValue = value;
        for (int i = 1; i <= guestValue; i++)
        {
            for (int j = 1; j <= peopleValue; j++)
            {
                if (i < 3)
                {
                    Destroy(Guest_up.transform.Find($"people{i}").transform.Find("People")
                        .transform.Find($"SimplePerson{j}").transform.
                            Find($"SimpleItem{value + 1}").gameObject);
                }
                else
                {
                    Destroy(Guest_down.transform.Find($"people{i}").transform.Find("People")
                        .transform.Find($"SimplePerson{j}").transform.
                            Find($"SimpleItem{value + 1}").gameObject);
                }

                /*
                if (i < 3)
                {
                    item.transform.SetParent(
                        Guest_up.transform.Find($"people{i}").transform.Find("People").transform.
                            Find($"SimplePerson{j}").transform);
                }
                else
                {
                    item.transform.SetParent(
                        Guest_down.transform.Find($"people{i}").transform.Find("People").transform.
                            Find($"SimplePerson{j}").transform);
                }*/
            }
        }
    }

    public void setItem(int value)
    {
        itemSelection = value;
    }
    public float getItemSelection()
    {
        return itemSelection;
    }
    public void Simulation()
    {
        if (Check())
        {
            SettingPanel.SetActive(false);
            OperatArea.SetActive(true);
            action = true;
            itemSelection = -1;
        }
    }
    public bool getAction()
    {
        return action;
    }
    public bool Check()
    {
        bool ok = true;
        for(int i = 1; i<=guestValue; i++)
        {
            for(int j = 1; j<=peopleValue; j++)
            {
                bool empty = true;
                for(float k = itemValue; k>=1; k--)
                {
                    GameObject targetItem = null;
                    if (i < 3)
                    {
                        targetItem = Guest_up.transform.Find($"people{i}").
                            Find("People").Find($"SimplePerson{j}").Find($"SimpleItem{k}").gameObject;
                    }
                    else
                    {
                        targetItem = Guest_down.transform.Find($"people{i}").
                            Find("People").Find($"SimplePerson{j}").Find($"SimpleItem{k}").gameObject;
                    }
                    if(targetItem.GetComponent<SimpleItemController>().GetValue() != -1)
                    {
                        empty = false;
                    }
                    if (!empty)
                    {
                        if(targetItem.GetComponent<SimpleItemController>().GetValue() == -1)
                        {
                            targetItem.GetComponent<UnityEngine.UI.Image>().color = 
                                new Color(1, 0, 0, 1);
                            StartCoroutine(ChangeColor(targetItem));
                            ok = false;
                            
                        }
                    }
                }
                
            }
            
        }
        return ok;
    }
    public IEnumerator ChangeColor(GameObject target)
    {
        yield return new WaitForSeconds(2);
        target.GetComponent<UnityEngine.UI.Image>().color =
                                new Color(1, 1, 1, 1);
        
    }
    
    public void UseAbility(int code)
    {
        if(code != 4 && !chefClick)
        {
            CreateFood(code);
        }
        else
        {
            chefClick = true;
        }
    }
    public void CreateFood(int code)
    {
        for (int i = 1; i <= guestValue; i++)
        {
            for (int j = 1; j <= peopleValue; j++)
            {
                bool empty = true;
                for (float k = 1; k <= itemValue; k++)
                {
                    if (empty)
                    {
                        GameObject targetItem = null;
                        if (i < 3)
                        {
                            targetItem = Guest_up.transform.Find($"people{i}").
                                Find("People").Find($"SimplePerson{j}").Find($"SimpleItem{k}").gameObject;
                        }
                        else
                        {
                            targetItem = Guest_down.transform.Find($"people{i}").
                                Find("People").Find($"SimplePerson{j}").Find($"SimpleItem{k}").gameObject;
                        }
                        if (targetItem.GetComponent<SimpleItemController>().GetValue() != -1)
                        {
                            empty = false;
                            if (targetItem.GetComponent<SimpleItemController>().GetValue() == code)
                            {
                                targetItem.GetComponent<UnityEngine.UI.Image>().sprite = null;
                                targetItem.GetComponent<SimpleItemController>().SetValue(-1);
                            }
                        }
                    }
                }

            }

        }
    }
    public void ChefClick(GameObject self)
    {
        if (chefClick)
        {
            if(self.transform.name.Equals("SimpleItem1"))
            {
                chefTimes++;
                self.GetComponent<UnityEngine.UI.Image>().sprite = null;
                self.GetComponent<SimpleItemController>().SetValue(-1);
                if (chefTimes == 2)
                {
                    chefTimes = 0;
                    chefClick = false;
                }
            }
            else if (self.transform.name.Equals("SimpleItem2"))
            {
                if(self.transform.parent.Find("SimpleItem1").GetComponent<SimpleItemController>().
                    GetValue() == -1)
                {
                    chefTimes++;
                    self.GetComponent<UnityEngine.UI.Image>().sprite = null;
                    self.GetComponent<SimpleItemController>().SetValue(-1);
                    if (chefTimes == 2)
                    {
                        chefClick = false;
                    }
                }
            }
            else
            {
                if (self.transform.parent.Find("SimpleItem1").GetComponent<SimpleItemController>().
                    GetValue() == -1 &&
                    self.transform.parent.Find("SimpleItem2").GetComponent<SimpleItemController>().
                    GetValue() == -1)
                {
                    chefTimes++;
                    self.GetComponent<UnityEngine.UI.Image>().sprite = null;
                    self.GetComponent<SimpleItemController>().SetValue(-1);
                    if (chefTimes == 2)
                    {
                        chefClick = false;
                    }
                }
            }
            
        }
    }
    public void Restart()
    {
        chefClick = false;
        action = false;
        itemSelection = -1;
        OperatArea.SetActive(false);
        SettingPanel.SetActive(true);
    }
}
