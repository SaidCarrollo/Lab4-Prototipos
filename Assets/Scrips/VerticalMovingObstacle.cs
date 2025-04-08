using UnityEngine;

public class VerticalMovingObstacle : MovingObstacle
{
    private bool movingUp = true;

    private void Update()
    {
        Move();
    }

    protected override void Move()
    {
        Transform target = movingUp ? pointB : pointA;
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            movingUp = !movingUp;
        }
    }
}