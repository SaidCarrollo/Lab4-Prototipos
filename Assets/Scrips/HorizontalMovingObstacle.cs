using UnityEngine;

public class HorizontalMovingObstacle : MovingObstacle
{
    private bool movingToB = true;

    private void Update()
    {
        Move();
    }

    protected override void Move()
    {
        Transform target = movingToB ? pointB : pointA;
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            movingToB = !movingToB;
        }
    }
}