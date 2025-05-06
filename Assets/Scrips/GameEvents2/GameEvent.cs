using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[CreateAssetMenu(fileName = "Void Event", menuName = "Game Events/Void Event")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners;

    private void OnEnable()
    {
        listeners = new List<GameEventListener>();
    }

    private void OnDisable()
    {
        listeners.Clear();
    }

    public void SetUp()
    {
        listeners = new List<GameEventListener>();
    }

    public void Raise()
    {
        foreach (GameEventListener sub in listeners)
        {
            sub.OnEventRaised();
        }
    }

    public void Register(GameEventListener newListener)
    {
        if (listeners.Contains(newListener)) return;

        listeners.Add(newListener);
    }

    public void Unregister(GameEventListener newListener)
    {
        if (!listeners.Contains(newListener)) return;

        listeners.Remove(newListener);
    }
}

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;

    [SerializeField] private UnityEvent response;

    private void OnEnable()
    {
        gameEvent.Register(this);
    }

    private void OnDisable()
    {
        gameEvent.Unregister(this);
    }

    public void OnEventRaised()
    {
        response?.Invoke();
    }
}
