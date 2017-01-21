﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcertManager : MonoBehaviour {
    // UI Graphic to display for each gesture.
    // Populate in-Editor with prefabs.
    public static ConcertManager instance;

    public List<RectTransform> GestureDisplays = new List<RectTransform>();
    public List<RectTransform> PoseDisplays = new List<RectTransform>();

    public BeatMapLevel currentLevel = new DummyLevel();
    private int eventIndex;
    private float nextEventTime;
    public float currentEventTime;
    private BeatMapEvent currentEvent;

    private float DELAY_TILL_SONG_START = 1.0f;

    public float gameTime = 0.0f;
    public float songTime = 0.0f;
    public float score;
    public float multiplier;

    public RewardUI reward;

    public GameObject CameraPositions;
    public Transform targetCameraTransform;

    public int cameraLevel;

    void Awake()
    {
        score = 0f;
        multiplier = 1;
        eventIndex = 0;
        currentEvent = currentLevel.GetEventAtIndex(eventIndex);
        currentEventTime = 0.0f;
        nextEventTime = currentEvent.eventLength;
    }

    void Start()
    {
        instance = this;

        AudioSource audio = Camera.main.GetComponent<AudioSource>();
        audio.clip = currentLevel.GetSong();
        audio.PlayDelayed(DELAY_TILL_SONG_START);

        reward = GameObject.FindObjectOfType<RewardUI>();

        SetCameraLevel(0, true);

    }

    public void SetCameraLevel(int level, bool instant)
    {
        targetCameraTransform = CameraPositions.transform.GetChild(level).transform;
        Debug.Log(targetCameraTransform.position);
        if(instant)
        {
            Camera.main.transform.position = targetCameraTransform.position;
            Camera.main.transform.rotation = targetCameraTransform.rotation;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q pressed.");
            AdvancePlayer();
        }

        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetCameraTransform.position, .02f);
        Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, targetCameraTransform.rotation, .02f);

        gameTime += Time.deltaTime;
        if(gameTime > DELAY_TILL_SONG_START)
        {
            songTime += Time.deltaTime;
        }
        if (gameTime > DELAY_TILL_SONG_START + currentLevel.GetDelayTillFirstEvent())
        {
            currentEventTime += Time.deltaTime;
        }
        else
        {
            return;
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
        if (gameTime < DELAY_TILL_SONG_START + currentLevel.GetDelayTillFirstEvent())
        {
            return;
        }


        if (currentEvent.GetEventType() == EventType.GESTURE_TYPE)
        {
            GestureBeatMapEvent gestureEvent = currentEvent as GestureBeatMapEvent;

            if(g == gestureEvent.gesture)
            {
                //Special case if its a CrowdWave Gesture
                if (gestureEvent.gesture == Gesture.CrowdWave)
                {
                    if (currentEventTime > gestureEvent.eventLength * 0.4f && currentEventTime > gestureEvent.eventLength * 0.6f)
                        gestureEvent.LogGesture();
                }
                else
                    gestureEvent.LogGesture();
            }

            //Add the score if the gesture is complete
            if(gestureEvent.EventFullfilled())
            {
                score += gestureEvent.GetReward() * multiplier;
                BroadcastReward();
            }
        }
    }

    public void PerformPose(Pose p)
    {
        if (gameTime < DELAY_TILL_SONG_START + currentLevel.GetDelayTillFirstEvent())
        {
            return;
        }


        if (currentEvent.GetEventType() == EventType.POSE_TYPE)
        {
            PoseBeatMapEvent poseEvent = currentEvent as PoseBeatMapEvent;

            if (p == poseEvent.CurrentPose())
            {
                poseEvent.LogPose();
            }

            if(poseEvent.EventFullfilled())
            {
                multiplier += poseEvent.GetMultiplier();
                AdvancePlayer();
            }
        }
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

        if (currentEvent.GetEventType() == EventType.POSE_TYPE)
        {
            PoseBeatMapEvent p = currentEvent as PoseBeatMapEvent;
            Debug.Log((int)p.CurrentPose());
            return PoseDisplays[(int)p.CurrentPose()];
        }
        return null;
    }

    public float GetTimeTillFirstEvent()
    {
        return DELAY_TILL_SONG_START + currentLevel.GetDelayTillFirstEvent() - gameTime;
    }

    public void BroadcastReward()
    {
        if(reward)
        {
            reward.Reward();
        }
    }

    public void AdvancePlayer()
    {
        cameraLevel++;
        SetCameraLevel(cameraLevel, false);

        if(reward)
        {
            reward.Advance();
        }
    }
}
