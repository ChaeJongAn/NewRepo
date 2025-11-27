using UnityEngine;
using UnityEngine.UI;

public class StatusIconUI : MonoBehaviour
{
    public Image iconImage;
    public Text timerText;

    public void Setup(StatusData data)
    {
        if (data.icon != null)
        {
            iconImage.sprite = data.icon;
        }
        UpdateTime(data.timeLeft);
    }

    public void UpdateTime(float timeLeft)
    {
        timerText.text = timeLeft.ToString("F1") + "s";
    }
}