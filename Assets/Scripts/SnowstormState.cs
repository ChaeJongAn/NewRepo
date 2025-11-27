using UnityEngine;

public class SnowstormState : EnemyState
{
    float _timer;
    float _duration = 2f;
    float _shootInterval = 0.08f;  // 더 빠르게
    float _shootTimer;

    public SnowstormState(EnemyAI enemy) : base(enemy) { }

    public override void Enter()
    {
        enemy.moveDir = Vector2.zero;
        _shootTimer = _shootInterval;
    }

    public override void Update()
    {
        _timer += Time.deltaTime;
        _shootTimer += Time.deltaTime;

        if (_shootTimer >= _shootInterval)
        {
            enemy.ShootSnowstorm(3, 60f);  // 3발씩, 60도 범위
            _shootTimer = 0f;
        }

        if (_timer >= _duration)
        {
            enemy.ChangeState(new IdleState(enemy));
        }
    }
}