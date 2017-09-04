using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] Transform exitPoint;
    [SerializeField] Transform[] wayPoints;
    [SerializeField] float navigationUpdate;

    int target = 0;
    private Transform enemy;
    private float navigationTime = 0;

    void Start()
    {
        enemy = GetComponent<Transform>();
        GameManager.Instance.RegisterEnemy(this);
    }

    void Update()
    {
        if (wayPoints != null)
        {
            navigationTime += Time.deltaTime;

            if (navigationTime > navigationUpdate)
            {
                if (target < wayPoints.Length)
                {
                    enemy.position = Vector2.MoveTowards(enemy.position, wayPoints[target].position, navigationTime);
                }
                else
                {
                    enemy.position = Vector2.MoveTowards(enemy.position, exitPoint.position, navigationTime);
                }

                navigationTime = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Checkpoint")
        {
            target += 1;
        }
        else if (other.tag == "Finish")
        {
            GameManager.Instance.UnregisterEnemy(this);
        }
    }
}
