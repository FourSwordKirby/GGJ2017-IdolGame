using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    POSE_TYPE,
    GESTURE_TYPE
}

public enum Gesture
{
    TEMPORARY_GESTURE_PLACEHOLDER_ENUM,
    TEMPORARY_GESTURE_PLACEHOLDER_ENUM2,
}

public abstract class BeatMapEvent
{
    public float eventLength;

    public BeatMapEvent(float eventLength)
    {
        this.eventLength = eventLength;
    }

    public virtual bool eventFullfilled()
    {
        return false;
    }

    public abstract EventType GetEventType();

    public virtual float GetReward()
    {
        return 0.0f;
    }

    public virtual RectTransform GetDisplay()
    {
        return null;
    }
}

public class GestureBeatMapEvent : BeatMapEvent
{
    public Gesture gesture;

    public GestureBeatMapEvent(float time, Gesture gesture) : base(time)
    {
        this.gesture = gesture;
    }

    public override EventType GetEventType()
    {
        return EventType.GESTURE_TYPE;
    }

    //TODO: Implement
    public override bool eventFullfilled()
    {
        return base.eventFullfilled();
    }

    public override RectTransform GetDisplay()
    {
        return base.GetDisplay();
    }
}

//TODO: Implement
public class PoseBeatMapEvent : BeatMapEvent
{
    public PoseBeatMapEvent(float time) : base(time)
    {

    }

    public override bool eventFullfilled()
    {
        return base.eventFullfilled();
    }

    public override EventType GetEventType()
    {
        return EventType.POSE_TYPE;
    }
}

