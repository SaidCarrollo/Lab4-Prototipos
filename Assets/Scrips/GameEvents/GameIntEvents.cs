using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "Int Event", menuName = "Game Events/Int Event")]
public class GameIntEvent : ScriptableObject
{
    private List<Action<int>> actionListeners = new List<Action<int>>();

    private List<GameIntEventListener> monoBehaviourListeners = new List<GameIntEventListener>();

    public void Raise(int value)
    {

        for (int i = actionListeners.Count - 1; i >= 0; i--)
        {
            actionListeners[i]?.Invoke(value);
        }

        for (int i = monoBehaviourListeners.Count - 1; i >= 0; i--)
        {
            monoBehaviourListeners[i]?.OnEventRaised(value);
        }
    }

    public void RegisterListener(Action<int> listener)
    {
        if (!actionListeners.Contains(listener))
            actionListeners.Add(listener);
    }

    public void UnregisterListener(Action<int> listener)
    {
        if (actionListeners.Contains(listener))
            actionListeners.Remove(listener);
    }

    public void Register(GameIntEventListener listener)
    {
        if (!monoBehaviourListeners.Contains(listener))
            monoBehaviourListeners.Add(listener);
    }

    public void Unregister(GameIntEventListener listener)
    {
        if (monoBehaviourListeners.Contains(listener))
            monoBehaviourListeners.Remove(listener);
    }
}

public class GameIntEventListener : MonoBehaviour
{
    [SerializeField] private GameIntEvent gameEvent;

    [SerializeField] private UnityEvent<int> response;

    private void OnEnable()
    {
        gameEvent.Register(this);
    }

    private void OnDisable()
    {
        gameEvent.Unregister(this);
    }

    public void OnEventRaised(int value)
    {
        response?.Invoke(value);
    }
}
