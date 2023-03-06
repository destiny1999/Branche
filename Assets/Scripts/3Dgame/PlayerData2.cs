using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData2
{
    public int Money;
    public List<GuestCardData2> CardList;
    public string Name;
    public int Dishes;
    public int MoneyBonus;
    public int GuestCapacity;
    public int CharacterCode;
    public PlayerData2(int _money, string _name, int _dishes, int _moneyBonus, List<GuestCardData2> _CardList, int _guestCapacity)
    {
        Money = _money;
        CardList = _CardList;
        Name = _name;
        Dishes = _dishes;
        MoneyBonus = _moneyBonus;
        GuestCapacity = _guestCapacity;
        
    }
    public PlayerData2()
    {

    }
}
