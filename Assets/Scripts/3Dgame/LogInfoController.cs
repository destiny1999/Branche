using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogInfoController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] RectTransform rect_logInfo = null;
    [SerializeField] RectTransform rect_content = null;
    [SerializeField] TMP_Text tmp_logInfo = null;
    public static LogInfoController Instance = null;
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
        //rect_content.sizeDelta = new Vector2(rect_content.sizeDelta.x, rect_logInfo.sizeDelta.y);
    }
    public IEnumerator ChangeHeight(int line)
    {
        //print("Into Change Height");
        //rect_content.transform.position = new Vector2(rect_content.transform.position.x,rect_logInfo.transform.position.y);
        //rect_content.sizeDelta = new Vector2(rect_content.sizeDelta.x, rect_logInfo.sizeDelta.y);
        float heightY = rect_logInfo.sizeDelta.y;
        while (heightY == rect_logInfo.sizeDelta.y)
        {
            //print("same");
            yield return 0;
        }
        rect_content.sizeDelta = new Vector2(rect_content.sizeDelta.x, rect_logInfo.sizeDelta.y);
        if(rect_logInfo.sizeDelta.y > 249.11)
        {
            //GetComponent<RectTransform>().anchoredPosition = new Vector2(posx, posy);
            rect_content.anchoredPosition = new Vector2(rect_content.anchoredPosition.x,
                rect_content.anchoredPosition.y + (float)48.3 * line);
            //rect_content.rect.y += 48.3;
        }
        //print("Change height");
        //print("content = " + rect_content.sizeDelta.y + " logInfo = " + rect_logInfo.sizeDelta.y);

    }
    public TMP_Text getLogInfo()
    {
        return tmp_logInfo;
    }
}
