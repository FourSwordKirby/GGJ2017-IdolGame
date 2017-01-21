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
        new GestureBeatMapEvent(2, Gesture.TEMPORARY_GESTURE_PLACEHOLDER_ENUM),
        new GestureBeatMapEvent(2, Gesture.TEMPORARY_GESTURE_PLACEHOLDER_ENUM2)
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