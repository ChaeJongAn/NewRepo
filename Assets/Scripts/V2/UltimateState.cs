using UnityEngine;

public class UltimateState : EnemyState
{
    float _chargeTimer;
    float _chargeDuration = 3f;      // 차징 시간
    float _maxScale = 5f;            // 최대 크기
    GameObject _chargingBullet;
    bool _fired;

    public UltimateState(EnemyAI enemy) : base(enemy) { }

    public override void Enter()
    {
        // 멈춤
        enemy.moveDir = Vector2.zero;

        // 차징 총알 생성
        _chargingBullet = Object.Instantiate(enemy.giantBulletPrefab, enemy.transform.position, Quaternion.identity);
        _chargingBullet.transform.localScale = Vector3.one * 0.5f; // 시작 크기

        BigIceBullet bullet = _chargingBullet.GetComponent<BigIceBullet>();
        if (bullet != null)
        {
            bullet.isCharging = true;
        }
    }

    public override void Update()
    {
        if (_fired) return;

        _chargeTimer += Time.deltaTime;

        // 차징 중 크기 증가
        if (_chargingBullet != null)
        {
            // 치르노 위치 따라다님
            _chargingBullet.transform.position = enemy.transform.position;

            // 크기 점점 커짐
            float t = _chargeTimer / _chargeDuration;
            float scale = Mathf.Lerp(0.5f, _maxScale, t);
            _chargingBullet.transform.localScale = Vector3.one * scale;
        }

        // 차징 완료 → 발사
        if (_chargeTimer >= _chargeDuration)
        {
            Fire();
        }
    }

    void Fire()
    {
        _fired = true;

        if (_chargingBullet != null && enemy.target != null)
        {
            BigIceBullet bullet = _chargingBullet.GetComponent<BigIceBullet>();
            if (bullet != null)
            {
                bullet.isCharging = false;

                // 플레이어 방향으로 발사
                Vector2 dir = (enemy.target.position - enemy.transform.position).normalized;
                bullet.direction = dir;
                bullet.speed = 3f;
            }
        }

        // 약간 딜레이 후 Idle로
        enemy.StartCoroutine(ReturnToIdle());
    }

    System.Collections.IEnumerator ReturnToIdle()
    {
        yield return new WaitForSeconds(0.5f);
        enemy.ChangeState(new IdleState(enemy));
    }

    public override void Exit()
    {
        // 발사 안하고 끝나면 삭제
        if (!_fired && _chargingBullet != null)
        {
            Object.Destroy(_chargingBullet);
        }
    }
}