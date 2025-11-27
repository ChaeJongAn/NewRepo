using UnityEngine;

public class Trap : MonoBehaviour
{
    public float lifetime = 5f;
    public int damage = 1;

    void OnEnable()
    {
        Invoke("Disable", lifetime);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    public virtual void ApplyEffect(PlayerHP player, StatusEffect status)
    {
        player.TakeDamage(damage);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHP player = other.GetComponent<PlayerHP>();
            StatusEffect status = other.GetComponent<StatusEffect>();

            if (player != null)
            {
                ApplyEffect(player, status);
            }

            gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}