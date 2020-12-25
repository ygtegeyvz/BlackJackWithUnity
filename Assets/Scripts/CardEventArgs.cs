﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEventArgs : EventArgs
{
    public int CardIndex { get;  set; }

    public CardEventArgs(int cardIndex)
    {
        CardIndex = cardIndex;
    }
}
