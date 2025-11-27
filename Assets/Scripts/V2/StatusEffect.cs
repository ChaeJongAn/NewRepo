using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour
{
    // 아이콘 (인스펙터에서 연결)
    public Sprite wetIcon;
    public Sprite freezeIcon;
    public Sprite burnIcon;

    // 활성화된 상태이상들
    public Dictionary<string, StatusData> activeEffects = new Dictionary<string, StatusData>();

    // 냉기 스택 (특수: 100 되면 빙결으로 변환)
    public float chillStack = 0f;
    public float chillDecayRate = 5f;

    // 내부
    PlayerMove _playerMove;
    float _originalSpeed;

    void Start()
    {
        _playerMove = GetComponent<PlayerMove>();
        _originalSpeed = _playerMove.speed;
    }

    void Update()
    {
        UpdateEffects();
        UpdateChill();
        ApplySpeedModifier();
    }

    void UpdateEffects()
    {
        // 삭제할 상태이상 목록
        List<string> toRemove = new List<string>();

        foreach (var pair in activeEffects)
        {
            pair.Value.timeLeft -= Time.deltaTime;

            if (pair.Value.timeLeft <= 0)
            {
                toRemove.Add(pair.Key);
            }
        }

        // 만료된 상태이상 제거
        foreach (string id in toRemove)
        {
            RemoveEffect(id);
        }
    }

    void UpdateChill()
    {
        // 빙결 중이면 냉기 안 쌓임
        if (HasEffect("freeze")) return;

        // 냉기 자연 감소
        if (chillStack > 0)
        {
            chillStack -= chillDecayRate * Time.deltaTime;
            if (chillStack < 0) chillStack = 0;
        }
    }

    void ApplySpeedModifier()
    {
        if (HasEffect("freeze"))
        {
            _playerMove.speed = 0;
        }
        else if (HasEffect("wet"))
        {
            _playerMove.speed = _originalSpeed * 0.5f;
        }
        else if (chillStack > 0)
        {
            // 냉기 스택에 비례해서 느려짐 (100%면 속도 0)
            float slowPercent = chillStack / 100f;
            _playerMove.speed = _originalSpeed * (1f - slowPercent);
        }
        else
        {
            _playerMove.speed = _originalSpeed;
        }
    }
    // 상태이상 추가
    public void AddEffect(string id, float duration)
    {
        Sprite icon = GetIcon(id);

        if (activeEffects.ContainsKey(id))
        {
            // 이미 있으면 시간 갱신
            activeEffects[id].timeLeft = duration;
        }
        else
        {
            // 새로 추가
            activeEffects[id] = new StatusData(id, duration, icon);
        }
    }

    // 상태이상 제거
    public void RemoveEffect(string id)
    {
        if (activeEffects.ContainsKey(id))
        {
            activeEffects.Remove(id);
        }
    }

    // 상태이상 있는지 확인
    public bool HasEffect(string id)
    {
        return activeEffects.ContainsKey(id);
    }

    // 남은 시간 가져오기
    public float GetTimeLeft(string id)
    {
        if (activeEffects.ContainsKey(id))
        {
            return activeEffects[id].timeLeft;
        }
        return 0f;
    }

    // 아이콘 가져오기
    Sprite GetIcon(string id)
    {
        switch (id)
        {
            case "wet": return wetIcon;
            case "freeze": return freezeIcon;
            case "burn": return burnIcon;
            default: return null;
        }
    }

    // 습기 적용
    public void ApplyWet(float duration = 999f)
    {
        AddEffect("wet", duration);
    }

    // 습기 해제
    public void RemoveWet()
    {
        RemoveEffect("wet");
    }

    // 냉기 추가
    public void AddChill(float amount)
    {
        chillStack += amount;

        if (chillStack >= 100f)
        {
            chillStack = 0f;
            Freeze();
        }
    }

    // 빙결
    public void Freeze(float duration = 2f)
    {
        RemoveEffect("wet"); // 습기 해제
        AddEffect("freeze", duration);
    }
}