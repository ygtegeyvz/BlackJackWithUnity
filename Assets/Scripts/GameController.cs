using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Deck player;
    public Deck dealer;
    public Deck deck;

    int dealerFirstCard = -1;

    public Button HitButton;
    public Button StickButton;
    public Button PlayAgainButton;

    public GameObject betPanel;

    public Text winnerText;
    public Text moneyText;

    void Start()
    {
      betPanel.active = true;
      StartGame();
    }

    // Kard çek.
    public void Hit()
    {
        //Destenin en üstündeki kartı alıp oyuncuya koyar.
        player.Push(deck.Pop());
        if (player.HandleValue() > 21)
        {
            //eğer elin değeri 21'den büyükse butonları pasifize et.
            HitButton.interactable = false;
            StickButton.interactable = false;
            StartCoroutine(DealersTurn());
        }
    }

    //Kal butonu
    public void Stick()
    {
        //Kart çekme ve kalma butonları pasif et.
        HitButton.interactable = false;
        StickButton.interactable = false;
        StartCoroutine(DealersTurn());
    }

    public void PlayAgain()
    {
        PlayAgainButton.interactable = false;

        player.GetComponent<DeckView>().Clear();
        dealer.GetComponent<DeckView>().Clear();
        deck.GetComponent<DeckView>().Clear();
        deck.CreateDeck();

        HitButton.interactable = true;
        StickButton.interactable = true;

        dealerFirstCard = -1;
        betPanel.active = true;

        winnerText.text = "";

        StartGame();
    }


    IEnumerator DealersTurn()
    {
        HitButton.interactable = false;
        StickButton.interactable = false;

        DeckView view = dealer.GetComponent<DeckView>();
        view.Toggle(dealerFirstCard, true);
        view.ShowCards();
        while (dealer.HandleValue() < 17)
        {
            HitDealer();
            yield return new WaitForSeconds(1f);
        }
    //    Debug.Log("log player=" +player.HandleValue());
     //   Debug.Log("log dealer=" + dealer.HandleValue());

        if (player.HandleValue() > 21 || (dealer.HandleValue() >= player.HandleValue() && dealer.HandleValue() <= 21))
        {
            winnerText.text = "Kaybettiniz.";
            Deck.money = Deck.money - Deck.betMoney;
            moneyText.text = "Para: " + Deck.money;
        }
        else if (dealer.HandleValue() > 21 || (player.HandleValue() <= 21 && player.HandleValue() > dealer.HandleValue()))
        {
            winnerText.text = "Kazandın!";
            Deck.money = Deck.money + Deck.betMoney;
            moneyText.text = "Para: " + Deck.money;
        }
        else
        {
            winnerText.text = "Kasa kazandı.Kumarda her zaman kasa kazanır..";
            Deck.money = Deck.money - Deck.betMoney;
            moneyText.text = "Para: " + Deck.money;
        }

        yield return new WaitForSeconds(1f);
        PlayAgainButton.interactable= true;
    }

    // Start is called before the first frame update
    void StartGame()
    {
        //Player ve Dealer'a 2 kart çekiliyor
        for (int i = 0; i < 2; i++)
        {
            player.Push(deck.Pop());
            HitDealer();
        }

        moneyText.text = "Para: " + Deck.money;
        //for (int i = 0; i < player.CardCount; i++)
        //{
        //Debug.Log("StartGame Player:"+player.GetCards().ElementAt(i));
        //}
        //for (int i = 0; i < dealer.CardCount; i++)
        //{
        //    Debug.Log("StartGame Dealer:" + dealer.GetCards().ElementAt(i));
        //}
        //for (int i = 0; i < deck.CardCount; i++)
        //{
        //    Debug.Log("StartGame Deck   :" + deck.GetCards().ElementAt(i));
        //}
    }

    public void HitDealer()
    {
        int card = deck.Pop();
        //Dealer'ın ilk kardını belirle
        if (dealerFirstCard < 0)
        {
            dealerFirstCard = card;
        }
        //dealer'a kart ver
        dealer.Push(card);
        if (dealer.CardCount >= 2)
        {
            //dealer'ın 2 veya 2 den fazla kartı varsa çevir
            DeckView view = dealer.GetComponent<DeckView>();
            view.Toggle(card, true);
        }

    }
}
