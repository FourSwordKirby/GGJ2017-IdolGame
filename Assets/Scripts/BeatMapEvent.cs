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
    RightArmPumps,
    LeftArmPumps,
    SimultaneousArmPumps,
    SlowWave,
    CrowdWave,
    Clap,
    LeftWave,
    RightWave,
    Idle
}

public enum Pose
{
    Neutral,
    KillerQueen, //(N+S)
    PierceTheHeavens, //(N+SE)
    Denial, //(NW+NWE)
    Naruto //(SW + SE)
}

public abstract class BeatMapEvent
{
    public float eventLength;

    public BeatMapEvent(float eventLength)
    {
        this.eventLength = eventLength;
    }

    public abstract bool EventFullfilled();

    public abstract EventType GetEventType();

    public virtual RectTransform GetDisplay()
    {
        return null;
    }
}

public class GestureBeatMapEvent : BeatMapEvent
{
    public Gesture gesture;
    public int gestureCount;
    public int loggedGestures;

    public GestureBeatMapEvent(float eventLength, Gesture gesture, int gestureCount) : base(eventLength)
    {
        this.gesture = gesture;
        //You can only do 
        if (this.gesture == Gesture.CrowdWave)
            this.gestureCount = 1;
        else
            this.gestureCount = gestureCount;
    }

    public override EventType GetEventType()
    {
        return EventType.GESTURE_TYPE;
    }

    public override bool EventFullfilled()
    {
        return loggedGestures >= gestureCount;
    }

    public override RectTransform GetDisplay()
    {
        return base.GetDisplay();
    }

    public void LogGesture()
    {
        loggedGestures++;
    }

    public float GetReward()
    {
        switch(gesture)
        {
            case Gesture.RightArmPumps:
            case Gesture.LeftArmPumps:
                return 100;
            case Gesture.SimultaneousArmPumps:
            case Gesture.Clap:
                return 200;
            case Gesture.SlowWave:
                return 500;
            case Gesture.CrowdWave:
                return 1000;
            default:
                return 0;
        }
    }
}

//TODO: Implement
public class PoseBeatMapEvent : BeatMapEvent
{
    public List<Pose> requiredPoses;
    public int loggedPoses;

    public PoseBeatMapEvent(float eventLength, List<Pose> poses) : base(eventLength)
    {
        requiredPoses = poses;
    }

    public override EventType GetEventType()
    {
        return EventType.POSE_TYPE;
    }

    public override bool EventFullfilled()
    {
        return requiredPoses.Count == 0;
    }

    public override RectTransform GetDisplay()
    {
        return base.GetDisplay();
    }

    public Pose CurrentPose()
    {
        if(requiredPoses.Count == 0)
        {
            return Pose.Neutral;
        }
        return requiredPoses[0];
    }

    public void LogPose()
    {
        loggedPoses++;
        if(requiredPoses.Count > 0)
        {
            requiredPoses.RemoveAt(0);
        }
    }

    public float GetMultiplier()
    {
        return 0.2f * loggedPoses;
    }
}

