using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : Singleton<TowerManager> {

    TowerButton towerButtonPressed;

    public void SelectedTower(TowerButton towerSelected)
    {
        towerButtonPressed = towerSelected;
    }

}
