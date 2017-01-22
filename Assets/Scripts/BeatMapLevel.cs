﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class BeatMapLevel {
    public abstract int GetTotalEvents();
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
        //new GestureBeatMapEvent(200f, Gesture.CrowdWave, 1),
        new GestureBeatMapEvent(5.6f, Gesture.LeftArmPumps, 1),
        new GestureBeatMapEvent(5.6f, Gesture.RightArmPumps, 1),
        new GestureBeatMapEvent(11.2f, Gesture.SlowWave, 1),
        new GestureBeatMapEvent(5.6f, Gesture.SimultaneousArmPumps, 1),
        new GestureBeatMapEvent(5.6f, Gesture.Clap, 1),
        new GestureBeatMapEvent(5.6f, Gesture.CrowdWave, 1),
        new GestureBeatMapEvent(5.6f, Gesture.Clap, 1),
        new PoseBeatMapEvent(3.0f, new List<Pose>(){Pose.KillerQueen}),
        new GestureBeatMapEvent(11.2f, Gesture.RightArmPumps, 1),

            
        //Testing game loop
        //new GestureBeatMapEvent(1.2f, Gesture.RightArmPumps, 1)
    };

    public override int GetTotalEvents()
    {
        return beatMap.Length;
    }

    public override BeatMapEvent GetEventAtIndex(int i)
    {
        return beatMap[i];
    }
    
    public override AudioClip GetSong()
    {
        clip = Resources.Load<AudioClip>("Audio/comet");
        return clip;
    }

    public override float GetDelayTillFirstEvent()
    {
        //return 1.0f;
        return 12.8f;
    }
}