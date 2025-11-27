using System.Collections.Generic;
using System.Linq;  
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectUI : MonoBehaviour
{
    public StatusEffect status;
    public Slider chillBar;
    public Transform iconParent;
    public GameObject iconPrefab;

    Dictionary<string, GameObject> activeIcons = new Dictionary<string, GameObject>();

    void Update()
    {
        if (status == null) return;

        UpdateChillBar();
        UpdateIcons();
    }

    void UpdateChillBar()
    {
        if (chillBar != null)
        {
            chillBar.value = status.chillStack / 100f;
        }
    }

    void UpdateIcons()
    {
        // Dictionary 복사해서 순회 (수정 중 충돌 방지)
        var effectsCopy = status.activeEffects.ToList();

        foreach (var pair in effectsCopy)
        {
            string id = pair.Key;
            StatusData data = pair.Value;

            if (!activeIcons.ContainsKey(id))
            {
                if (iconPrefab != null && iconParent != null)
                {
                    GameObject icon = Instantiate(iconPrefab, iconParent);
                    StatusIconUI iconUI = icon.GetComponent<StatusIconUI>();
                    if (iconUI != null)
                    {
                        iconUI.Setup(data);
                    }
                    activeIcons[id] = icon;
                }
            }
            else
            {
                StatusIconUI iconUI = activeIcons[id].GetComponent<StatusIconUI>();
                if (iconUI != null)
                {
                    iconUI.UpdateTime(data.timeLeft);
                }
            }
        }

        // 제거된 상태이상 아이콘 삭제
        var iconsCopy = activeIcons.ToList();

        foreach (var pair in iconsCopy)
        {
            if (!status.activeEffects.ContainsKey(pair.Key))
            {
                Destroy(pair.Value);
                activeIcons.Remove(pair.Key);
            }
        }
    }
}