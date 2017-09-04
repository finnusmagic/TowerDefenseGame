using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButton : MonoBehaviour {

    [SerializeField] GameObject towerObject;

    public GameObject TowerObject
    {
        get
        {
            return towerObject;
        }
    }
}
