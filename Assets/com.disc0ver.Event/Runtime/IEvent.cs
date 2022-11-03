using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Disc0ver.Event
{
    public interface IBaseEvent { }

    /// <summary>
    /// 事件接口，T 为事件类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEvent<T>: IBaseEvent
    {
        public abstract T EventType { get; }
    }

    /// <summary>
    /// 事件监听基础接口
    /// </summary>
    public interface IBaseEventListener { }

    public interface IEventListener<Event> : IBaseEventListener where Event: struct, IBaseEvent
    {
        /// <summary>
        /// 接收到事件消息时的处理函数
        /// </summary>
        /// <param name="newEvent"></param>
        public void OnReceiveEvent(Event newEvent);
    }

    public abstract class EventManager
    {
        private Dictionary<Type, List<IBaseEventListener>> eventSubscriptDict = new Dictionary<Type, List<IBaseEventListener>>();

        public void AddListener<Event>(IBaseEventListener eventListener) where Event: struct, IEvent<EventType>
        {
            if(!eventSubscriptDict.TryGetValue(typeof(Event), out List<IBaseEventListener> subscriptList))
            {
                subscriptList = new List<IBaseEventListener>();
            }

            subscriptList.Add(eventListener);
            eventSubscriptDict[typeof(Event)] = subscriptList;
        }

        public void RemoveListener<Event>(IBaseEventListener eventListener) where Event: struct, IEvent<EventType>
        {
            if (!eventSubscriptDict.TryGetValue(typeof(Event), out List<IBaseEventListener> subscriptList))
            {
                return;
            }

            subscriptList.Remove(eventListener);
            if(subscriptList.Count == 0)
            {
                eventSubscriptDict.Remove(typeof(Event));
            }
        }

        public void OnBroadCastEvent<Event>(Event newEvent) where Event : struct, IEvent<EventType>
        {
            if(!eventSubscriptDict.TryGetValue(typeof(Event), out List<IBaseEventListener> subscriptList))
            {
                return;
            }

            for(int i = subscriptList.Count - 1; i >= 0; i--)
            {
                IEventListener<Event> listener = subscriptList[i] as IEventListener<Event>;
                listener.OnReceiveEvent(newEvent);
            }
        }
    }
}
