using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Disc0ver.Event
{
    /// <summary>
    /// EventManager 基类
    /// </summary>
    public abstract class AEventManager
    {
        private readonly Dictionary<string, Type> _eventDict = new Dictionary<string, Type>();
        private readonly Dictionary<Type, List<IBaseEventListener>> _eventSubscriptDict = new Dictionary<Type, List<IBaseEventListener>>();

        public void AddListener<TEvent>(IBaseEventListener eventListener) where TEvent: IBaseEvent
        {
            AddListener(typeof(TEvent), eventListener);
        }

        public void AddListener(Type type, IBaseEventListener eventListener)
        {
            if(!_eventSubscriptDict.TryGetValue(type, out List<IBaseEventListener> subscriptList))
            {
                subscriptList = new List<IBaseEventListener>();
            }

            subscriptList.Add(eventListener);
            _eventSubscriptDict[type] = subscriptList;
        }

        public void RemoveListener<TEvent>(IBaseEventListener eventListener) where TEvent: IBaseEvent
        {
            if (!_eventSubscriptDict.TryGetValue(typeof(TEvent), out List<IBaseEventListener> subscriptList))
            {
                return;
            }

            subscriptList.Remove(eventListener);
            if(subscriptList.Count == 0)
            {
                _eventSubscriptDict.Remove(typeof(TEvent));
            }
        }

        public void OnBroadCastEvent<TEvent>(TEvent newEvent) where TEvent : IBaseEvent
        {
            if(!_eventSubscriptDict.TryGetValue(typeof(TEvent), out List<IBaseEventListener> subscriptList))
            {
                return;
            }

            for(int i = subscriptList.Count - 1; i >= 0; i--)
            {
                IEventListener<TEvent> listener = subscriptList[i] as IEventListener<TEvent>;
                if (listener == null)
                {
                    IEventsListener eventsListener = subscriptList[i] as IEventsListener;
                    eventsListener?.OnReceiveEvent(newEvent);
                }
                else
                {
                    listener.OnReceiveEvent(newEvent);
                }
            }
        }
        
        public void RegisterEvents()
        {
            var typesWithEventIdAttribute =
                // Note the AsParallel here, this will parallelize everything after.
                from a in AppDomain.CurrentDomain.GetAssemblies().AsParallel()
                from t in a.GetTypes()
                where t.IsDefined(typeof(EventIdAttribute), false)
                select t;

            foreach (var type in typesWithEventIdAttribute)
            {
                EventIdAttribute attribute = type.GetCustomAttribute<EventIdAttribute>();
                if(attribute == null)
                    continue;
                _eventDict.Add(attribute.value, type);
            }
        }

        public Type GetEventType(string eventId)
        {
            if (_eventDict.TryGetValue(eventId, out Type type))
            {
                return type;
            }

            return null;
        }
    }

    /// <summary>
    /// 全局 EventManager,使用 EventManager.Instance 调用相关方法
    /// </summary>
    public class EventManager : AEventManager
    {
        private static EventManager _instance;

        public static EventManager Instance => _instance ??= new EventManager();

        public EventManager()
        {
            RegisterEvents();
        }
    }
}