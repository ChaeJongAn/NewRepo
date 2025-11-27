using UnityEngine;

public class IceExplosion : MonoBehaviour
{
    public int damage = 2;
    public float freezeDuration = 3f;
    public float lifetime = 0.5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHP player = other.GetComponent<PlayerHP>();
            StatusEffect status = other.GetComponent<StatusEffect>();

            if (player != null)
            {
                player.TakeDamage(damage);
            }
            if (status != null)
            {
                status.Freeze(freezeDuration);
            }
        }
    }
}