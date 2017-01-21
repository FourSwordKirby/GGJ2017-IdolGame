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
        new GestureBeatMapEvent(2, Gesture.ArmPumps, 3),
        new GestureBeatMapEvent(2, Gesture.SlowWave, 3),
        new GestureBeatMapEvent(2, Gesture.CrowdWave, 1),
        new PoseBeatMapEvent(2, new List<Pose>(){Pose.KillerQueen})
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
        return 12.8f;
    }
}