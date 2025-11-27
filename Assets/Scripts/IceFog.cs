using UnityEngine;

public class IceFog : Fog
{
    public float chillPerSecond = 1f; // √ ¥Á 1% ≥√±‚

    protected override void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StatusEffect status = other.GetComponent<StatusEffect>();
            if (status != null)
            {
                status.AddChill(chillPerSecond * Time.deltaTime);
            }
        }
    }
}