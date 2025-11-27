using UnityEngine;

public class IdleState : EnemyState
{
    float _timer;

    public IdleState(EnemyAI enemy) : base(enemy) { }

    public override void Enter()
    {
        PickRandomDirection();
    }

    public override void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= enemy.changeInterval)
        {
            PickRandomDirection();
            _timer = 0f;
        }
    }

    void PickRandomDirection()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        enemy.moveDir = new Vector2(x, y).normalized * enemy.moveSpeed;
    }
}