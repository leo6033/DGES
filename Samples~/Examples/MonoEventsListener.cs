using Disc0ver.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoEventsListener : MonoBehaviour, IEventsListener
{
    public List<Type> ListenEvents => _eventId;

    private List<Type> _eventId;

    [SerializeReference] private List<GameStage> _listenEvents;

    private void OnEnable()
    {
        _eventId = new List<Type>();

        foreach (var stage in _listenEvents)
        {
            Type type = EventManager.Instance.GetEventType(stage.ToString());
            if(type != null)
                _eventId.Add(type);
        }
        
        foreach (var type in ListenEvents)
        {
            EventManager.Instance.AddListener(type, this);
        }
    }

    private void Start()
    {
        EventManager.Instance.OnBroadCastEvent(new GameStartEvent(){startMessage = "start"});
    }

    public void OnReceiveEvent(IBaseEvent newEvent)
    {
        Debug.Log($"Receive Event, type {newEvent.Type}");
    }
}
