using UnityEngine;
using UnityEngine.UI;

public class EnemyHpUI : MonoBehaviour
{
    public Slider hpBar;
    public EnemyHP enemyHp;

    void Start()
    {
        if (enemyHp != null)
        {
            hpBar.maxValue = enemyHp.maxHp;
        }
    }

    void Update()
    {
        if (enemyHp != null)
        {
            hpBar.value = enemyHp.currentHp;
        }
    }
}