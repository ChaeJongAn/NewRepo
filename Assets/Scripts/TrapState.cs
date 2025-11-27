using UnityEngine;

public class TrapState : EnemyState
{
    float _timer;
    float _duration = 1f;
    bool _placed;

    public TrapState(EnemyAI enemy) : base(enemy) { }

    public override void Enter()
    {
        enemy.moveDir = Vector2.zero;
        _placed = false;
    }

    public override void Update()
    {
        _timer += Time.deltaTime;

        // 0.5초에 설치
        if (_timer >= 0.5f && !_placed)
        {
            enemy.PlaceBigTrap();
            _placed = true;
        }

        if (_timer >= _duration)
        {
            enemy.ChangeState(new IdleState(enemy));
        }
    }
}