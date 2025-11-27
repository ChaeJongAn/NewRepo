using UnityEngine;

public class FogState : EnemyState
{
    float _timer;
    float _duration = 2f;
    float _spawnInterval = 0.1f;
    float _spawnTimer;

    public FogState(EnemyAI enemy) : base(enemy) { }

    public override void Enter()
    {
        enemy.moveDir = Vector2.zero;
    }

    public override void Update()
    {
        _timer += Time.deltaTime;
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= _spawnInterval)
        {
            enemy.SpawnFog();
            _spawnTimer = 0f;
        }

        if (_timer >= _duration)
        {
            enemy.ChangeState(new IdleState(enemy));
        }
    }
}