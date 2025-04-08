using UnityEngine;

using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    public enum CollectableType { Coin, Heart }

    [SerializeField] protected CollectableType type;
    [SerializeField] protected int value = 1;
    [SerializeField] protected float rotationSpeed = 100f;
    [SerializeField] protected float floatAmplitude = 0.5f;
    [SerializeField] protected float floatFrequency = 1f;

    private Vector3 startPosition;

    protected virtual void Start()
    {
        startPosition = transform.position;
    }

    protected virtual void Update()
    {
        // Animación de flotación
        transform.position = startPosition + new Vector3(0, Mathf.Sin(Time.time * floatFrequency) * floatAmplitude, 0);

        // Rotación
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Collect(player);
            }
        }
    }

    public void Collect(Player player)
    {
        ApplyEffect(player);
        PlayCollectionEffect();
        Destroy(gameObject);
    }

    protected abstract void ApplyEffect(Player player);

    protected virtual void PlayCollectionEffect()
    {
        // Aquí podrías añadir efectos de partículas, sonido, etc.
        Debug.Log($"Recolectado {type} con valor {value}");
    }
}