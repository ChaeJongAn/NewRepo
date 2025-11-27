using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("이동")]
    public float moveSpeed = 2f;
    public float rushSpeed = 8f;
    public float changeInterval = 2f;
    public float rushDuration = 1f;

    [Header("쿨타임")]
    public float rushCooldown = 3f;
    public float snowstormCooldown = 5f;
    public float trapStateCooldown = 8f;
    public float fogCooldown = 10f;

    [Header("타겟")]
    public Transform target;

    [Header("작은 함정")]
    public GameObject trapPrefab;
    public float trapDistance = 1.5f;
    public int trapPoolSize = 40;

    [Header("대왕 함정")]
    public GameObject bigTrapPrefab;
    public int bigTrapPoolSize = 5;

    [Header("총알")]
    public GameObject bulletPrefab;
    public int bulletPoolSize = 100;

    [Header("안개")]
    public GameObject fogPrefab;
    public int fogPoolSize = 20;
    public float fogSpreadRange = 2f;

    [Header("필살기")]
    public GameObject giantBulletPrefab;
    public float ultimateChargeTime = 10f; // 총알 안 맞으면 이 시간 후 필살기

    // 내부 변수
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Vector2 moveDir;

    List<GameObject> _trapPool = new List<GameObject>();
    List<GameObject> _bigTrapPool = new List<GameObject>();
    List<GameObject> _bulletPool = new List<GameObject>();
    List<GameObject> _fogPool = new List<GameObject>();

    EnemyState _currentState;
    float _rushTimer;
    float _snowstormTimer;
    float _trapStateTimer;
    float _fogTimer;
    float _ultimateTimer; // 필살기 타이머
    Vector3 _lastTrapPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _lastTrapPos = transform.position;

        // 풀 생성
        CreatePool(trapPrefab, trapPoolSize, _trapPool);
        CreatePool(bigTrapPrefab, bigTrapPoolSize, _bigTrapPool);
        CreatePool(bulletPrefab, bulletPoolSize, _bulletPool);
        CreatePool(fogPrefab, fogPoolSize, _fogPool);

        ChangeState(new IdleState(this));
    }

    void CreatePool(GameObject prefab, int size, List<GameObject> pool)
    {
        if (prefab == null) return;

        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    void Update()
    {
        _currentState?.Update();

        // UltimateState가 아닐 때만 필살기 타이머 체크
        if (!(_currentState is UltimateState))
        {
            _ultimateTimer += Time.deltaTime;

            if (_ultimateTimer >= ultimateChargeTime)
            {
                _ultimateTimer = 0f;
                ChangeState(new UltimateState(this));
                return;
            }
        }

        // Idle일 때만 쿨타임 체크
        if (_currentState is IdleState)
        {
            CheckCooldowns();
        }

        // 똥 흘리기
        CheckTrapDrop();
    }

    // 플레이어가 총알 맞으면 호출 (타이머 리셋)
    public void OnPlayerHitByBullet()
    {
        _ultimateTimer = 0f;
    }

    void CheckCooldowns()
    {
        _rushTimer += Time.deltaTime;
        _snowstormTimer += Time.deltaTime;
        _trapStateTimer += Time.deltaTime;
        _fogTimer += Time.deltaTime;

        if (_snowstormTimer >= snowstormCooldown)
        {
            ChangeState(new SnowstormState(this));
            _snowstormTimer = 0f;
        }
        else if (_fogTimer >= fogCooldown)
        {
            ChangeState(new FogState(this));
            _fogTimer = 0f;
        }
        else if (_trapStateTimer >= trapStateCooldown)
        {
            ChangeState(new TrapState(this));
            _trapStateTimer = 0f;
        }
        else if (_rushTimer >= rushCooldown)
        {
            ChangeState(new RushState(this));
            _rushTimer = 0f;
        }
    }

    void CheckTrapDrop()
    {
        if (moveDir == Vector2.zero) return;

        float dist = Vector3.Distance(transform.position, _lastTrapPos);
        float distance = (_currentState is RushState) ? trapDistance * 0.3f : trapDistance;

        if (dist >= distance)
        {
            PlaceTrap();
            _lastTrapPos = transform.position;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveDir;
    }

    public void ChangeState(EnemyState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StatusEffect status = other.gameObject.GetComponent<StatusEffect>();
            if (status != null)
            {
                status.Freeze(2f);
            }
        }
    }

    GameObject GetFromPool(List<GameObject> pool)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }
        return null;
    }

    public GameObject GetBullet()
    {
        return GetFromPool(_bulletPool);
    }

    void PlaceTrap()
    {
        GameObject trap = GetFromPool(_trapPool);
        if (trap != null)
        {
            trap.transform.position = transform.position;
            trap.SetActive(true);
        }
    }

    public void PlaceBigTrap()
    {
        GameObject trap = GetFromPool(_bigTrapPool);
        if (trap != null)
        {
            trap.transform.position = transform.position;
            trap.SetActive(true);
        }
    }

    public void SpawnFog()
    {
        GameObject fog = GetFromPool(_fogPool);
        if (fog != null)
        {
            Vector2 randomPos = (Vector2)transform.position + Random.insideUnitCircle * fogSpreadRange;
            fog.transform.position = randomPos;
            fog.SetActive(true);
        }
    }

    public void ShootSnowstorm(int bulletCount, float spreadAngle)
    {
        if (target == null) return;

        Vector2 targetDir = (target.position - transform.position).normalized;
        float baseAngle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;

        for (int i = 0; i < bulletCount; i++)
        {
            float randomAngle = baseAngle + Random.Range(-spreadAngle / 2f, spreadAngle / 2f);
            float rad = randomAngle * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            GameObject bullet = GetBullet();
            if (bullet != null)
            {
                bullet.transform.position = transform.position;
                bullet.SetActive(true);
                bullet.GetComponent<Bullet>().direction = dir;
                bullet.GetComponent<Bullet>().speed = Random.Range(2f, 5f);
            }
        }
    }
}