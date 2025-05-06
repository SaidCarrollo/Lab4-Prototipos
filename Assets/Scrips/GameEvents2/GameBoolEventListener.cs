using UnityEngine;
using UnityEngine.Events;

public class GameBoolEventListener : MonoBehaviour
{
    [SerializeField] private GameBoolEvent gameEvent;
    [SerializeField] private UnityEvent<bool> response;

    private void OnEnable()
    {
        if (gameEvent != null)
            gameEvent.Register(this);
    }

    private void OnDisable()
    {
        if (gameEvent != null)
            gameEvent.Unregister(this);
    }

    public void OnEventRaised(bool value)
    {
        response?.Invoke(value);
    }
}
