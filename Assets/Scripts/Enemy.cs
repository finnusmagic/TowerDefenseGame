using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] Transform exitPoint;
    [SerializeField] Transform[] wayPoints;
    [SerializeField] float navigationUpdate;
    [SerializeField] int healthPoints;

    int target = 0;
    private Transform enemy;
    private Collider2D enemyCollider;
    private Animator animator;
    private float navigationTime = 0;
    bool isDead = false;

    public bool IsDead
    {
        get
        {
            return isDead;
        }
    }

    void Start()
    {
        enemy = GetComponent<Transform>();
        enemyCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        GameManager.Instance.RegisterEnemy(this);
    }

    void Update()
    {
        if (wayPoints != null && !isDead)
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
        else if (other.tag == "Projectile")
        {
            Projectile newProjectile = other.gameObject.GetComponent<Projectile>();
            EnemyHit(newProjectile.AttackStrength);
            Destroy(other.gameObject);
        }
    }

    public void EnemyHit(int hitpoints)
    {
        if (healthPoints - hitpoints > 0)
        {
            healthPoints -= healthPoints;
            animator.Play("Hurt");
        }
        else
        {
            animator.SetTrigger("didDie");
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
        enemyCollider.enabled = false;
    }
}
