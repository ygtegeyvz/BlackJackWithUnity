using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Deck))]
public class DeckView : MonoBehaviour
{
    Deck deck;
    public Vector3 start;
    public float cardOffSet;
    public GameObject cardPrefab;
    public bool faceUp = false;
    public bool reverseLayerOrder = false;

    Dictionary<int, CardView> fetchedCards;

    // Start is called before the first frame update
    void Awake()
    {
        fetchedCards = new Dictionary<int, CardView>();
        deck = GetComponent<Deck>();
        ShowCards();
        deck.CardRemoved += deck_CardRemoved;
        deck.CardAdded += deck_CardAdded;

    }

    public void Clear()
    {
        deck.Reset();
        foreach (CardView view in fetchedCards.Values)
        {
            Destroy(view.Card);
        }
        fetchedCards.Clear();
    }

    public void Toggle(int card, bool isFaceUp)
    {
        fetchedCards[card].IsFaceUp = isFaceUp;
    }

    private void deck_CardRemoved(object sender, CardEventArgs e)
    {
        if (fetchedCards.ContainsKey(e.CardIndex))
        {
            Destroy(fetchedCards[e.CardIndex].Card);
            fetchedCards.Remove(e.CardIndex);
        }
    }

    private void deck_CardAdded(object sender, CardEventArgs e)
    {
        float co = cardOffSet * deck.CardCount;
        Vector3 temp = start + new Vector3(co, 0f);
        AddCard(temp, e.CardIndex, deck.CardCount);
    }

    public void ShowCards()
    {
        int cardCount = 0;
        if (deck.hasCards)
        {
            foreach (var i in deck.GetCards())
            {
                float co = cardOffSet * cardCount;
                Vector3 temp = start + new Vector3(co, 0f);
                AddCard(temp, i, cardCount);
                cardCount++;
            }
        }
    }

    void AddCard(Vector3 position, int cardIndex, int positionalIndex)
    {

        if (fetchedCards.ContainsKey(cardIndex))
        {
            if (!faceUp)
            {
                CardModel model = fetchedCards[cardIndex].Card.GetComponent<CardModel>();
                model.ToggleFace(fetchedCards[cardIndex].IsFaceUp);
            }
            return;
        }

        GameObject cardCopy = (GameObject)Instantiate(cardPrefab);
        cardCopy.transform.position = position;

        CardModel cardModel = cardCopy.GetComponent<CardModel>();
        cardModel.cardIndex = cardIndex;
        cardModel.ToggleFace(faceUp);

        SpriteRenderer spriteRenderer = cardCopy.GetComponent<SpriteRenderer>();
        if (reverseLayerOrder)
        {
            spriteRenderer.sortingOrder = 51 - positionalIndex;

        }
        else
        {
            spriteRenderer.sortingOrder = positionalIndex;
        }
            fetchedCards.Add(cardIndex, new CardView(cardCopy));  
            //Debug.Log("Hand Value = "+ deck.HandleValue());
    }

    // Update is called once per frame
    void Update()
    {
        ShowCards();
    }
}
