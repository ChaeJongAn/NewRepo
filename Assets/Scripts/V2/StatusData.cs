using UnityEngine;

[System.Serializable]
public class StatusData
{
    public string id;           // "wet", "freeze", "burn" 등
    public float duration;      // 지속시간
    public float timeLeft;      // 남은시간
    public Sprite icon;         // UI 아이콘

    public StatusData(string id, float duration, Sprite icon)
    {
        this.id = id;
        this.duration = duration;
        this.timeLeft = duration;
        this.icon = icon;
    }
}