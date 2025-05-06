using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Bool Event", menuName = "Game Events/Bool Event")]
public class GameBoolEvent : ScriptableObject
{
    private List<GameBoolEventListener> listeners = new List<GameBoolEventListener>();

    public void Raise(bool value)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(value);
        }
    }

    public void Register(GameBoolEventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void Unregister(GameBoolEventListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}
