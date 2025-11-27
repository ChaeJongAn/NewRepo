using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 3f;
    public Vector2 direction;
    public int damage = 1;
    public float lifetime = 5f;

    protected virtual void OnEnable()
    {
        Invoke("Disable", lifetime);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    protected virtual void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    protected virtual void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    public virtual void ApplyEffect(PlayerHP player, StatusEffect status)
    {
        player.TakeDamage(damage);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
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