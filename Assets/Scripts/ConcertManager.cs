using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcertManager : MonoBehaviour {
    // UI Graphic to display for each gesture.
    // Populate in-Editor with prefabs.
    public List<RectTransform> GestureDisplays = new List<RectTransform>();

    public BeatMapLevel currentLevel = new DummyLevel();
    private int eventIndex;
    private float nextEventTime;
    private float currentEventTime;
    private BeatMapEvent currentEvent;

    void Awake()
    {
        eventIndex = 0;
        currentEvent = currentLevel.GetEventAtIndex(eventIndex);
        currentEventTime = 0.0f;
        nextEventTime = currentEvent.eventLength;
    }

    void Start()
    {
    }

    void Update()
    {
        currentEventTime += Time.deltaTime;
        float t = currentEventTime;

        // Progress level based on time
        if (t > nextEventTime)
        {
            eventIndex++;
            currentEvent = currentLevel.GetEventAtIndex(eventIndex);
            currentEventTime = 0.0f;
            nextEventTime = currentEvent.eventLength;
        }
    }

    public void PerformGesture(Gesture g)
    {
        throw new System.NotImplementedException();
    }

    public void PerformPose(int ImplementMePlease)
    {
        throw new System.NotImplementedException();
    }

    public RectTransform GetPromptDisplay()
    {
        if (currentEvent.GetEventType() == EventType.GESTURE_TYPE)
        {
            GestureBeatMapEvent e = currentEvent as GestureBeatMapEvent;
            return GestureDisplays[(int)e.gesture];
        }
        return null;
    }
}
