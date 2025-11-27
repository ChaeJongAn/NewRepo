using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float spawnInterval = 1f;
    public Transform target;
    public int poolSize = 10;

    // 총알 보관함
    List<GameObject> _bulletPool = new List<GameObject>();
    float _timer;

    void Start()
    {
        // 시작할 때 총알 미리 만들어두기
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            _bulletPool.Add(bullet);
        }
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= spawnInterval)
        {
            SpawnBullet();
            _timer = 0f;
        }
    }

    void SpawnBullet()
    {
        if (target == null) return;

        // 쉬고 있는 총알 가져오기
        GameObject bullet = GetBullet();
        if (bullet == null) return;

        // 위치 설정하고 활성화
        bullet.transform.position = transform.position;
        bullet.SetActive(true);

        // 플레이어 방향으로 발사
        Vector2 dir = (target.position - transform.position).normalized;
        bullet.GetComponent<Bullet>().direction = dir;
    }

    // 비활성화된 총알 찾는 함수
    GameObject GetBullet()
    {
        for (int i = 0; i < _bulletPool.Count; i++)
        {
            if (!_bulletPool[i].activeInHierarchy)
            {
                return _bulletPool[i];
            }
        }
        return null;
    }
}