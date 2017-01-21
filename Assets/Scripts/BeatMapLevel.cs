using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class BeatMapLevel {
    
    public abstract BeatMapEvent GetEventAtIndex(int i);
    public abstract AudioClip GetSong();
    public abstract float GetDelayTillFirstEvent();
}

public class DummyLevel : BeatMapLevel
{
    private AudioClip clip;

    // The Level
    private BeatMapEvent[] beatMap =
    {
        new PoseBeatMapEvent(5, new List<Pose>(){Pose.KillerQueen}),
        new GestureBeatMapEvent(5.6f, Gesture.ArmPumps, 1),
        new GestureBeatMapEvent(5.6f, Gesture.SlowWave, 1),
        new GestureBeatMapEvent(5.6f, Gesture.ArmPumps, 1),
        new GestureBeatMapEvent(5.6f, Gesture.SlowWave, 1),
        new GestureBeatMapEvent(5.6f, Gesture.CrowdWave, 1),
        new GestureBeatMapEvent(2.9f, Gesture.ArmPumps, 1),
        new GestureBeatMapEvent(3.7f, Gesture.ArmPumps, 1),
        new PoseBeatMapEvent(5, new List<Pose>(){Pose.KillerQueen})
    };

    public override BeatMapEvent GetEventAtIndex(int i)
    {
        return beatMap[i];
    }
    
    public override AudioClip GetSong()
    {
        clip = Resources.Load<AudioClip>("Audio/Melt");
        return clip;
    }

    public override float GetDelayTillFirstEvent()
    {
        return 2.0f;
    }
}