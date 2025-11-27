using UnityEngine;

public class IceBullet : Bullet
{
    public float chillAmount = 20f;

    public override void ApplyEffect(PlayerHP player, StatusEffect status)
    {
        base.ApplyEffect(player, status);

        // 필살기 타이머 리셋
        EnemyAI enemy = FindFirstObjectByType<EnemyAI>();
        if (enemy != null)
        {
            enemy.OnPlayerHitByBullet();
        }

        if (status != null)
        {
            if (status.HasEffect("wet"))
            {
                status.Freeze();
            }
            else
            {
                status.AddChill(chillAmount);
            }
        }
    }
}