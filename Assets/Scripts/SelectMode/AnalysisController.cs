using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnalysisController : MonoBehaviour
{
    [SerializeField] TMP_Text Level = null;
    [SerializeField] TMP_Text WinPlace = null;
    [SerializeField] TMP_Text LosePlace = null;
    [SerializeField] TMP_Text Rate = null;

    void Awake()
    {
        var all = User.Instance.userData.allGame;
        var lose = all - User.Instance.userData.winningGame;
        var win = User.Instance.userData.winningGame;
        var rate = all > 0 ? win / all : 0;
        Level.text = User.Instance.userData.lv+"";
        WinPlace.text = win+"";
        LosePlace.text = lose+"";
        Rate.text = rate + "";
    }
}
