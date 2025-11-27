using UnityEngine;

public class BigIceTrap : MonoBehaviour
{
    public float lifetime = 10f;
    public GameObject explosionPrefab;  // Æø¹ß È÷Æ®¹Ú½º

    void OnEnable()
    {
        Invoke("Disable", lifetime);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Frog"))
        {
            Explode();
        }
    }

    void Explode()
    {
        // Æø¹ß È÷Æ®¹Ú½º »ý¼º
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        Debug.Log("¾óÀ½²É Æø¹ß!");
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}