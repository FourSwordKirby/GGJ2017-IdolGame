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
    public float currentEventTime;
    private BeatMapEvent currentEvent;

    private float DELAY_TILL_SONG_START = 3.0f;

    public float gameTime = 0.0f;
    public float songTime = 0.0f;

    void Awake()
    {
        eventIndex = 0;
        currentEvent = currentLevel.GetEventAtIndex(eventIndex);
        currentEventTime = 0.0f;
        nextEventTime = currentEvent.eventLength;
    }

    void Start()
    {
        AudioSource audio = Camera.main.GetComponent<AudioSource>();
        Debug.Log(audio);
        Debug.Log(currentLevel.GetSong());
        audio.clip = currentLevel.GetSong();
        audio.PlayDelayed(DELAY_TILL_SONG_START);
        Debug.Log(audio);
    }

    void Update()
    {
        gameTime += Time.deltaTime;
        if(gameTime > DELAY_TILL_SONG_START)
        {
            songTime += Time.deltaTime;
        }
        if (gameTime > DELAY_TILL_SONG_START + currentLevel.GetDelayTillFirstEvent())
        {
            currentEventTime += Time.deltaTime;
        }

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
        if (gameTime < DELAY_TILL_SONG_START + currentLevel.GetDelayTillFirstEvent())
        {
            return null;
        }

        if (currentEvent.GetEventType() == EventType.GESTURE_TYPE)
        {
            GestureBeatMapEvent e = currentEvent as GestureBeatMapEvent;
            return GestureDisplays[(int)e.gesture];
        }
        return null;
    }

    public float GetTimeTillFirstEvent()
    {
        return DELAY_TILL_SONG_START + currentLevel.GetDelayTillFirstEvent() - gameTime;
    }
}
