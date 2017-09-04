﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButton : MonoBehaviour {

    [SerializeField] Tower towerObject;
    [SerializeField] Sprite dragSprite;
    [SerializeField] int towerPrice;

    public Tower TowerObject
    {
        get
        {
            return towerObject;
        }
    }

    public Sprite DragSprite
    {
        get
        {
            return dragSprite;
        }
    }

    public int TowerPrice
    {
        get
        {
            return towerPrice;
        }
    }
}
