using UnityEngine;

public class BigIceBullet : Bullet
{
    public float chillAmount = 50f;
    public bool isCharging = true;

    protected override void Update()
    {
        if (isCharging) return;
        base.Update();
    }

    protected override void OnBecameInvisible()
    {
        if (isCharging) return;
        base.OnBecameInvisible();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (isCharging) return;

        if (other.CompareTag("Player"))
        {
            PlayerHP player = other.GetComponent<PlayerHP>();
            StatusEffect status = other.GetComponent<StatusEffect>();

            if (player != null)
            {
                ApplyEffect(player, status);
            }

            Destroy(gameObject);
        }
    }

    public override void ApplyEffect(PlayerHP player, StatusEffect status)
    {
        base.ApplyEffect(player, status);

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