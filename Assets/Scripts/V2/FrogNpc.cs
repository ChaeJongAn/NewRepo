using UnityEngine;

public class FrogNPC : MonoBehaviour
{
    public float moveSpeed = 2f;
    public bool isWild = true;  // 야생 개구리인지 (수집 가능)

    Vector2 _moveDir;
    float _timer;
    Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        PickRandomDirection();
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= 2f)
        {
            PickRandomDirection();
            _timer = 0f;
        }
    }

    void FixedUpdate()
    {
        _rb.linearVelocity = _moveDir * moveSpeed;
    }

    void PickRandomDirection()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        _moveDir = new Vector2(x, y).normalized;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어가 수집 (야생일 때만)
        if (other.CompareTag("Player") && isWild)
        {
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory != null)
            {
                inventory.AddFrog();
                Debug.Log("개구리 수집!");
                Destroy(gameObject);
            }
        }
        // 총알 대신 맞기 (소환된 개구리)
        else if (other.CompareTag("Bullet") && !isWild)
        {
            other.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}