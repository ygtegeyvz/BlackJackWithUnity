using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] faces;
    public Sprite back;

    public int cardIndex;

    public void ToggleFace(bool showFace)
    {
        if (showFace)
        {
            spriteRenderer.sprite = faces[cardIndex];
        }
        else
        {
            spriteRenderer.sprite = back;
        }    
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
