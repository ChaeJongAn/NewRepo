using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int maxHp = 10;
    public int currentHp;
    private bool isDead = false;

    void Start()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHp -= damage;
        Debug.Log("Enemy HP: " + currentHp);

        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        if (GameManager.instance != null)
        {
            GameManager.instance.OnEnemyDeath();
        }

        Destroy(gameObject);
    }
}