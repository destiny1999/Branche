using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectDisappear : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject target = null;
    [SerializeField] float second = 0f;
    [SerializeField] TMP_Text message = null;
    public static ObjectDisappear Instance;

    void Awake()
    {
        Instance = this;
        target = this.gameObject;
    }

    void Start()
    {
        /*target = this.gameObject;
        Invoke("disappear", second);*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void disappear()
    {
        target.SetActive(false);
    }
    public void callDisappear()
    {
        Invoke("disappear", second);
    }
    public void setMessage(string text)
    {
        message.text = text;
    }
}
