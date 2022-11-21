using System;
using System.Collections.Generic;
using UnityEngine;

namespace Disc0ver.Event
{
    /// <summary>
    /// 事件监听基础接口
    /// </summary>
    public interface IBaseEventListener { }

    /// <summary>
    /// 单事件监听，一个 IEventListener 支持监听一个事件
    /// 假如类 A 要监听 EventA, EventB
    /// </summary>
    /// <typeparam name="TEvent"> 需要监听的事件 </typeparam>
    /// <example>
    /// <code>  class A: IEventListener&lt;EventA&gt;  IEventListener&lt;EventB&gt; </code>
    /// </example>
    public interface IEventListener<in TEvent> : IBaseEventListener where TEvent: IBaseEvent
    {
        /// <summary>
        /// 接收到事件消息时的处理函数
        /// </summary>
        /// <param name="newEvent"></param>
        public void OnReceiveEvent(TEvent newEvent);
    }

    /// <summary>
    /// 多事件监听，在 ListenEvents 中加入需要监听的事件
    /// 由类内部去做各种 Event 的处理
    /// </summary>
    public interface IEventsListener : IBaseEventListener
    {
        /// <summary>
        /// 监听的事件
        /// </summary>
        public abstract List<Type> ListenEvents { get; }

        /// <summary>
        /// 接收到事件消息时的处理函数
        /// </summary>
        /// <param name="newEvent"></param>
        public void OnReceiveEvent(IBaseEvent newEvent);

    }
}