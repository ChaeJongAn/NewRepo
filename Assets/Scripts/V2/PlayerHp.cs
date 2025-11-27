using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public int maxHp = 5;
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
        Debug.Log("Player HP: " + currentHp);

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
            GameManager.instance.OnPlayerDeath();
        }

        gameObject.SetActive(false);
    }
}