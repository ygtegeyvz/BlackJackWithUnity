using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    List<int> cards;

    public static int money=1000;
    public static int betMoney = 0; 
    public Button[] betButtons;
    public GameObject betPanel;
    public bool isGameDeck;

    public bool hasCards
    {
        get { return cards != null && cards.Count > 0; }
    }

    //Buton hareketleri için listenerların tanımlanması
    void OnEnable()
    {
        for (int i = 0; i < betButtons.Length; i++)
        {
            int closureIndex = i; 
            betButtons[closureIndex].onClick.AddListener(() => TaskOnClick(closureIndex));
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    //Bet butonlarına tıklanınca
    public void TaskOnClick(int buttonIndex)
    {
        if (buttonIndex == 0)
        {
            betMoney = 100;

        }
        else if (buttonIndex == 1)
        {
            betMoney = 200;
        }
        else if (buttonIndex == 2)
        {
            betMoney = 500;
        }

        betPanel.active = false;
    }

    public int CardCount
    {
        get
        {
            if (cards == null)
            {
                return 0;
            }
            else
                return cards.Count;
        }
    }
    //Kartların değerinin hesaplanması
    public int HandleValue()
    {
        int total= 0;
        int aces = 0;

        foreach (int card in GetCards())
        {
            int cardRank = card % 13;

            if (cardRank<=8)
            {
                cardRank += 2;
                total = total + cardRank;
            }
            else if(cardRank > 8 && cardRank<12)
            {
                cardRank = 10;
                total = total + cardRank;
            }
            else
            {
                aces++;
            }

            for (int i = 0; i < aces; i++)
            {
                if (total + 11 <= 21)
                {
                    total = total + 11;
                }
                else
                {
                    total = total + 1;
                }
            }
        }
        return total;
    }

   

    public event CardEventHandler CardRemoved;
    public event CardEventHandler CardAdded;

    public IEnumerable<int> GetCards()
    {
        foreach (var i in cards)
        {
            yield return i;
        }
    }

    public int Pop()
    {
        //En üstteki kartı çek
        int temp = cards[0];
        //desteden çekilen kartı çıkar.
        cards.RemoveAt(0);
        if (CardRemoved != null)
        {
            CardRemoved(this,new CardEventArgs(temp));
        }
        return temp;
    }

    public void Push(int card)
    {
        cards.Add(card);

        if (CardAdded != null)
        {
            CardAdded(this, new CardEventArgs(card));
        }
    }

    //Desteyi oluşturuyor.
    public void CreateDeck()
    {
        //Tüm desteyi eğer varsa temizle.
        cards.Clear();

        //52'ye kadar değer ekler
        for (int i = 0; i < 52; i++)
        {
            cards.Add(i);
        }

        //Değerleri randomlaştırır.Deste karılmış olur.
        int count = cards.Count;
        while (count > 1)
        {
            count--;
            int random = Random.Range(0, count + 1);
            int temp = cards[random];
            cards[random] = cards[count];
            cards[count] = temp;
        }
    }
    //Kartları temizle
    public void Reset()
    {
        cards.Clear();
    }


    // Start is called before the first frame update
    void Awake()
    {
        cards = new List<int>();
        if (isGameDeck)
        {
            CreateDeck();
        }
    }
}
