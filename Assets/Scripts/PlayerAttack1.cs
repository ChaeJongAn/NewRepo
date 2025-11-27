using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack1 : MonoBehaviour
{
    public float attackRange = 1f;
    public float attackDuration = 0.1f;
    public int damage = 1;
    public GameObject hitboxPrefab;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            SlashAttack();
        }
    }

    void SlashAttack()
    {
        // 마우스 방향 계산
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 dir = (mousePos - (Vector2)transform.position).normalized;

        // 히트박스 위치 (플레이어 앞)
        Vector2 spawnPos = (Vector2)transform.position + dir * attackRange;

        // 히트박스 생성
        GameObject hitbox = Instantiate(hitboxPrefab, spawnPos, Quaternion.identity);
        hitbox.GetComponent<SlashHitbox>().damage = damage;

        // 잠깐 후 삭제
        Destroy(hitbox, attackDuration);
    }
}