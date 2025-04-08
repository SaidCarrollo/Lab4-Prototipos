using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    [SerializeField] private Color obstacleColor;
    [SerializeField] private SpriteRenderer obstacleSprite;

    private void Start()
    {
        if (obstacleSprite != null)
        {
            obstacleSprite.color = obstacleColor;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null && player.GetCurrentColor() != obstacleColor)
        {
            player.TakeDamage(1);
        }
    }
}
public abstract class MovingObstacle : Obstacle
{
    [SerializeField] protected Transform pointA;
    [SerializeField] protected Transform pointB;
    [SerializeField] protected float moveSpeed = 2f;

    protected abstract void Move();
}