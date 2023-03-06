using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RecordItemData : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TMP_Text Date = null;
    [SerializeField] TMP_Text BonuseAndGuest = null;
    [SerializeField] TMP_Text Money = null;
    [SerializeField] TMP_Text Score = null;
    [SerializeField] Image Rank = null;
    [SerializeField] Sprite one = null;
    [SerializeField] Sprite two = null;
    [SerializeField] Sprite three = null;
    [SerializeField] Sprite four = null;

    public void SetUp(string date, int bonuse, int guest, int money, int score, int rank)
    {
        Date.text = date;
        BonuseAndGuest.text = bonuse + " / " + guest + "";
        Money.text = money + "";
        Score.text = score + "";
        Rank.sprite = rank == 1 ? one : rank == 2 ? two : rank == 3 ? three : four;
    }
}
