using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GuestCardData2
{
    public int CardNum; // 1
    public string CardName; // GC1 - GC40
    public int Money;
    public int People; // 1-3
    public List<Queue> ItemList;
    public int CompletedPeople;
    public GuestCardData2(int _CardNum, string _CardName, int _Money, int _People, List<Queue> _ItemList)
    {
        CardNum = _CardNum;
        CardName = _CardName;
        Money = _Money;
        People = _People;
        ItemList = _ItemList;
        CompletedPeople = 0;
    }
    public GuestCardData2()
    {

    }
}
