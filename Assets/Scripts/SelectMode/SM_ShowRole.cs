using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SM_ShowRole : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image showRole = null;
    [SerializeField] Sprite iOffLine = null;
    [SerializeField] Sprite iOnline = null;
    [SerializeField] Sprite iExit = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
       // Debug.Log(this.name);
        if (this.name.Equals("OffLine"))
        {
            showRole.sprite = iOffLine;
         //   showRole.sprite = saMode.GetSprite("offline");
        }
        else if (this.name.Equals("Online"))
        {
            showRole.sprite = iOnline;
            //showRole.sprite = saMode.GetSprite("online");
        }
        else if (this.name.Equals("Exit"))
        {
            showRole.sprite = iExit;
            //showRole.sprite = saMode.GetSprite("brancheF");
        }
        showRole.gameObject.SetActive(true);
        this.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().color = new Color(0,0,0,1);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //showRole.gameObject.SetActive(false);
        this.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().color = new Color(0.1415094f, 0.1328319f, 0.1328319f, 1);
    }
}
