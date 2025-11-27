using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float changeInterval = 2f;

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

        if (_timer >= changeInterval)
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
}