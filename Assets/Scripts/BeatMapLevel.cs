using System;
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
        new GestureBeatMapEvent(6.84f, Gesture.RightArmPumps, 1), //16
        new GestureBeatMapEvent(5.6f, Gesture.SimultaneousArmPumps, 1), //21.6
        new GestureBeatMapEvent(1.278f, Gesture.LeftArmPumps, 1), //22.878
        new GestureBeatMapEvent(6.86f, Gesture.SlowWave, 1), //29.738
        new GestureBeatMapEvent(5.14f, Gesture.CrowdWave, 1), //34.878
        new PoseBeatMapEvent(1.720f, new List<Pose>(){Pose.KillerQueen}), //36.598
        new GestureBeatMapEvent(10.295f, Gesture.SlowWave, 1), //46.893
        new GestureBeatMapEvent(6.86f, Gesture.Clap, 1), //53.753 //Man clapping to the rhythm would hurt but posing for 7 seconds would be awkward
        new GestureBeatMapEvent(6.86f, Gesture.SlowWave, 1), //60.613
        new GestureBeatMapEvent(6.86f, Gesture.CrowdWave, 1), //67.473
        new PoseBeatMapEvent(3.43f, new List<Pose>(){Pose.KillerQueen}), //70.903
        new GestureBeatMapEvent(6.86f, Gesture.CrowdWave, 1), //77.763
        new GestureBeatMapEvent(5.14f, Gesture.SlowWave, 1), //82.903
        new PoseBeatMapEvent(1.720f, new List<Pose>(){Pose.KillerQueen}), //84.623
        new GestureBeatMapEvent(6.86f, Gesture.LeftArmPumps, 1), //91.483 //Should this be symmetric or repeated?
        new GestureBeatMapEvent(5.6f, Gesture.SimultaneousArmPumps, 1), //97.083
        new GestureBeatMapEvent(1.278f, Gesture.RightArmPumps, 1), //98.361
        new GestureBeatMapEvent(6.795f, Gesture.SlowWave, 1), //105.155 //mfw there's half a beat less
        new GestureBeatMapEvent(5.14f, Gesture.CrowdWave, 1), //110.361
            
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
        return 9.16f;
    }
}