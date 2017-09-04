using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : Singleton<TowerManager> {

    private TowerButton towerButtonPressed;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider.tag == "BuildSite")
            {
                hit.collider.tag = "BuildSiteFull";
                PlaceTower(hit);
            }
        }
    }

    public void PlaceTower(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && towerButtonPressed != null)
        {
            GameObject newTower = Instantiate(towerButtonPressed.TowerObject);
            newTower.transform.position = hit.transform.position;
        }
    }

    public void SelectedTower(TowerButton towerSelected)
    {
        towerButtonPressed = towerSelected;
    }

}
