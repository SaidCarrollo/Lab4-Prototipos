using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Victory Event", menuName = "Game Events/Victory Event")]
public class GameVictoryEvent : ScriptableObject
{
    private List<GameVictoryEventListener> listeners = new List<GameVictoryEventListener>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(GameVictoryEventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(GameVictoryEventListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}
public class GameVictoryEventListener : MonoBehaviour
{
    [SerializeField] private GameVictoryEvent gameEvent;
    [SerializeField] private UnityEvent response;

    private void OnEnable()
    {
        if (gameEvent != null)
            gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        if (gameEvent != null)
            gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        response?.Invoke();
    }
}