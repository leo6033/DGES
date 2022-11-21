using Disc0ver.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class GameStartEvent: AGameStageEvent
{
    public override GameStage EventType => GameStage.GameStart;
    public string startMessage;
}

public class GameEndEvent : AGameStageEvent
{
    public override GameStage EventType => GameStage.GameEnd;
    public string endMessage;
}
