using UnityEngine;

public class SlashHitbox : MonoBehaviour
{
    public int damage = 1;
    public float bulletClearRadius = 1f;

    void OnEnable()
    {
        ClearOneProjectile();
    }

    void ClearOneProjectile()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, bulletClearRadius);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Bullet") || hit.CompareTag("Trap"))
            {
                hit.gameObject.SetActive(false);
                return;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHP>().TakeDamage(damage);
        }
        else if (other.CompareTag("Bullet") || other.CompareTag("Trap"))
        {
            other.gameObject.SetActive(false);
        }
    }
}