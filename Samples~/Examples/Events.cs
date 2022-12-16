using Disc0ver.Event;
using System;

public enum GameStage
{
    GameStart,
    GameEnd,
}

public abstract class AGameStageEvent : IEvent<GameStage>
{
    public abstract GameStage EventType { get; }
    public Type Type => GetType();
}

[EventId(nameof(GameStage.GameStart))]
public class GameStartEvent: AGameStageEvent
{
    public override GameStage EventType => GameStage.GameStart;
    public string startMessage;
}

[EventId(nameof(GameStage.GameEnd))]
public class GameEndEvent : AGameStageEvent
{
    public override GameStage EventType => GameStage.GameEnd;
    public string endMessage;
}
