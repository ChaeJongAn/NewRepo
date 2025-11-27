using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour
{
    public Slider hpBar;
    public Text hpText;
    public PlayerHP playerHp;

    void Start()
    {
        if (playerHp != null)
        {
            hpBar.maxValue = playerHp.maxHp;
            UpdateUI();
        }
    }

    void Update()
    {
        if (playerHp != null)
        {
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        hpBar.value = playerHp.currentHp;
        hpText.text = playerHp.currentHp + " / " + playerHp.maxHp;
    }
}