using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int target = 0;
    public Transform exitPoint;
    public Transform[] wayPoints;
    public float navigationUpdate;

    private Transform enemy;
    private float navigationTime = 0;

    void Start()
    {
        enemy = GetComponent<Transform>();
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
            GameManager.Instance.RemoveEnemyFromScreen();
            Destroy(gameObject);
        }
    }
}
