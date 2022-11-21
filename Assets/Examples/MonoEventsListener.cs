using Disc0ver.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoEventsListener : MonoBehaviour, IEventsListener
{
    public List<Type> ListenEvents => _listenEvents;

    [SerializeReference] private List<Type> _listenEvents;

    private void OnEnable()
    {
        _listenEvents = new List<Type>();
        _listenEvents.Add(typeof(GameStartEvent));
        foreach (var type in _listenEvents)
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
