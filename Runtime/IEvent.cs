
namespace Disc0ver.Event
{
    public interface IBaseEvent { }

    /// <summary>
    /// 事件接口，T 为事件类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEvent<out T>: IBaseEvent
    {
        public abstract T EventType { get; }
    }
}
