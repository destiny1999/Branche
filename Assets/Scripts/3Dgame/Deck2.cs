using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck2 : MonoBehaviour
{
    public static Deck2 Instance;
    public static Queue<GuestCardData2> GuestCard = new Queue<GuestCardData2>();
    static Dictionary<int, GuestCardData2> guestCard = new Dictionary<int, GuestCardData2>();
    //static bool created = false;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {       
        
        /*GuestCard = new Queue<GuestCardData2>();
        guestCard = new Dictionary<int, GuestCardData2>();*/
    }
    public Queue<GuestCardData2> getGuestCard()
    {
        return GuestCard;
    }
    public void Create()
    {
        guestCard = new Dictionary<int, GuestCardData2>();
        int[] ID = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                    11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
                    21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
                    31, 32, 33, 34, 35, 36, 37, 38, 39, 40 };
        int[] P = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                    1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                    1, 1, 2, 2, 2, 2, 2, 2, 2, 2,
                    2, 2, 2, 2, 3, 3, 3, 3, 3, 3};
        int[] M = { 2, 2, 3, 2, 2, 3, 2, 2, 3, 3,
                    5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
                    5, 5, 5, 5, 5, 5, 5, 5, 6, 6,
                    6, 6, 6, 6, 7, 7, 7, 7, 7, 7};
        int[][] I =                             //item   1 steak    2 cake  3 juice  
        {
            new int[] { 2, 3},
            new int[] { 3, 2},
            new int[] { 2, 3 , 1},
            new int[] { 3, 1},
            new int[] { 2, 1},
            new int[] { 3, 1, 2},
            new int[] { 1, 2},
            new int[] { 1, 3},
            new int[] { 1, 2, 3},
            new int[] { 3, 2, 1},
            //- - - - - - - - - - - - - 
            new int[] { 3, 3, 1, 2},

            new int[] { 2, 3, 3, 1},

            new int[] { 1, 2, 3, 3},

            new int[] { 1, 2, 3, 1},

            new int[] { 1, 2, 2, 3},

            new int[] { 3, 1, 2, 2},

            new int[] { 1, 1, 2, 3},

            new int[] { 2, 3, 1, 2},

            new int[] { 3, 1, 2, 3},

            new int[] { 2, 2, 3, 1},

            new int[] { 3, 1, 1, 2},

            new int[] { 2, 3, 1, 1},

            new int[] { 1, 1, 1},
            new int[] { 3},

            new int[] { 3},
            new int[] { 2, 2, 2},

            new int[] { 3, 3, 3},
            new int[] { 2},

            new int[] { 1, 1},
            new int[] { 2, 2},

            new int[] { 3, 3},
            new int[] { 2, 2},

            new int[] { 1, 1},
            new int[] { 2, 2},

            new int[] { 2, 1, 3},
            new int[] { 1, 1, 3},

            new int[] { 3, 2, 1},
            new int[] { 2, 2, 1},

            new int[] { 3, 1, 2},
            new int[] { 1, 1, 2},

            new int[] { 1, 2, 3},
            new int[] { 2, 2, 3},

            new int[] { 1, 3, 2},
            new int[] { 3, 3, 2},

            new int[] { 2, 3, 1},
            new int[] { 2, 2, 1},

            new int[] { 3, 2},
            new int[] { 2, 1},
            new int[] { 1, 3},

            new int[] { 2, 3},
            new int[] { 3, 1},
            new int[] { 1, 2},

            new int[] { 3, 1},
            new int[] { 1, 2},
            new int[] { 2, 3},

            new int[] { 2, 1},
            new int[] { 3, 2},
            new int[] { 1, 3},

            new int[] { 1, 3},
            new int[] { 3, 2},
            new int[] { 2, 1},

            new int[] { 3, 2},
            new int[] { 1, 2},
            new int[] { 2, 3},
        };

        int index = 0;
        int itemIndex = 0;
        //Dictionary<int, GuestCardData2> guestCard = new Dictionary<int, GuestCardData2>();
        for(int i = 1; i<=40; i++, index++)
        {
            int cardNum = ID[index];
            string cardName = "GC" + cardNum;
            int money = M[index];
            int people = P[index];
            List<Queue> item = new List<Queue>();
            for(int p = 0; p<people; p++, itemIndex++)
            {
                Queue q = new Queue();
                for(int r = 0; r <I[itemIndex].Length; r++)
                {
                    q.Enqueue(I[itemIndex][r]);
                }
                item.Add(q);
            }
            GuestCardData2 card = new GuestCardData2(cardNum, cardName, money, people, item);
            guestCard.Add(i, card);
        }
        Debug.Log("Create Card Finished");
        Debug.Log("guestCard card num = " + guestCard.Count);
    }
    public void SetUp(int[] ShuffleOGC, int[] ShuffleGC)
    {
        Debug.Log("created card num = " + guestCard.Count);
        
        for (int i = 0; i < ShuffleOGC.Length; i++)
        {
            int key = ShuffleOGC[i];
            
            GuestCard.Enqueue(guestCard[key]);
        }
        for (int i = 0; i < 30; i++)
        {
            int key = ShuffleGC[i];
            GuestCard.Enqueue(guestCard[key]);
        }
    }
    public int[] Shuffle(int min, int max)
    {
        int[] r = new int[max - min + 1];

        for (int i = min, pos = 0; i <= max; i++, pos++)
        {
            r[pos] = i;
        }
        for (int i = 0; i < r.Length; i++)
        {
            int temp = Random.Range(min, max + 1);
            int current = r[i];
            r[i] = r[temp - min];
            r[temp - min] = current;

        }
        return r;
    }
    void ShoeTest()
    {
        for (int i = 0; i < 40; i++)
        {
            GuestCardData2 card = GuestCard.Dequeue();
            Debug.Log("CardNum = " + card.CardNum);
            Debug.Log("CardName = " + card.CardName);
            Debug.Log("Money = " + card.Money);
            Debug.Log("People = " + card.People);
            Debug.Log("Item = ");
            List<Queue> item = card.ItemList;
            for(int p = 0; p<card.People; p++)
            {
                Queue q = item[p];
                string food = "";
                
                while (q.Count > 0)
                {
                    int code = (int)q.Dequeue();
                    if(code == 1)
                    {
                        food += "steak ";
                    }
                    else if(code == 2)
                    {
                        food += "cake ";
                    }
                    else
                    {
                        food += "juice ";
                    }
                }
                Debug.Log(food);
            }
        }

            
        
        
    }
}
