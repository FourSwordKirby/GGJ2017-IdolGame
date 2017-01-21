using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class BeatMapLevel {
    
    public abstract BeatMapEvent GetEventAtIndex(int i);

}

public class DummyLevel : BeatMapLevel
{
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
}