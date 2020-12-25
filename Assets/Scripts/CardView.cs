using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardView : MonoBehaviour
{
    public GameObject Card { get;   set; }
    public bool IsFaceUp { get;  set; }

    public CardView(GameObject card)
    {
        Card = card;
        IsFaceUp = false;
    }
}
