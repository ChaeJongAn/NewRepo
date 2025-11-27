using UnityEngine;

public class IceTrap : Trap
{
    public float chillAmount = 30f;

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