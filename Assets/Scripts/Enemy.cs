using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] Transform exitPoint;
    [SerializeField] Transform[] wayPoints;
    [SerializeField] float navigationUpdate;
    [SerializeField] int healthPoints;
    [SerializeField] int rewardAmount;

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
            GameManager.Instance.RoundEscaped += 1;
            GameManager.Instance.TotalEscaped += 1;
            GameManager.Instance.UnregisterEnemy(this);
            GameManager.Instance.IsWaveOver();
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
            GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Hit);
            animator.Play("Hurt");
        }
        else
        {
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
        animator.SetTrigger("didDie");
        enemyCollider.enabled = false;
        GameManager.Instance.TotalKilled += 1;
        GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Death);
        GameManager.Instance.AddMoney(rewardAmount);
        GameManager.Instance.IsWaveOver();
    }
}
