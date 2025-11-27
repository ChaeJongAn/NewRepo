using UnityEngine;

public class Knife : MonoBehaviour
{
    public float speed = 8f;
    public Vector2 direction;
    public int damage = 1;

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHP>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Bullet"))
        {
            other.gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Trap"))
        {
            other.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}