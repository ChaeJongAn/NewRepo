using UnityEngine;

public class RushState : EnemyState
{
    float _timer;

    public RushState(EnemyAI enemy) : base(enemy) { }

    public override void Enter()
    {
        if (enemy.target != null)
        {
            Vector2 dir = (enemy.target.position - enemy.transform.position).normalized;
            enemy.moveDir = dir * enemy.rushSpeed;
        }
    }

    public override void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= enemy.rushDuration)
        {
            enemy.ChangeState(new IdleState(enemy));
        }
    }

    public override void Exit()
    {
        enemy.moveDir = Vector2.zero;
    }
}