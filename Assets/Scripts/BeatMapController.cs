using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Gesture
{
    TEMPORARY_GESTURE_PLACEHOLDER_ENUM,
    TEMPORARY_GESTURE_PLACEHOLDER_ENUM2,
}

public abstract class BeatMapEvent
{
    public float time;

    public BeatMapEvent(float time)
    {
        this.time = time;
    }

    public virtual bool eventFullfilled()
    {
        return false;
    }
}

public class GestureBeatMapEvent : BeatMapEvent
{
    public Gesture gesture;

    public GestureBeatMapEvent(float time, Gesture gesture) : base(time)
    {
        this.gesture = gesture;
    }

    //TODO: Implement
    public override bool eventFullfilled()
    {
        return base.eventFullfilled();
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
}

public class BeatMapController : MonoBehaviour {

    // The Level
    private BeatMapEvent[] beatMap =
    {
        new GestureBeatMapEvent(10, Gesture.TEMPORARY_GESTURE_PLACEHOLDER_ENUM),
        new GestureBeatMapEvent(15, Gesture.TEMPORARY_GESTURE_PLACEHOLDER_ENUM2)
    };

    private int eventIndex;
    private float nextEventTime;
    private BeatMapEvent currentEvent;

	void Start () {
        nextEventTime = beatMap[0].time;
	}
	
	void Update () {
        // Replace with time of music
        float t = Time.time;

        // Progress level based on time
        if (t > nextEventTime)
        {
            currentEvent = beatMap[eventIndex];

            eventIndex++;
            nextEventTime = beatMap[eventIndex].time;
        }

        if (currentEvent != null)
        {
            if (currentEvent.eventFullfilled())
            {
                //TODO: Add Points Here
            }
        }
	}
}
