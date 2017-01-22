using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public BeatMapEvent currentEvent;

    private float DELAY_TILL_SONG_START = 1.0f;
    private int MAX_LEVEL = 4;

    public float gameTime = 0.0f;
    public float songTime = 0.0f;
    public float score;
    public float multiplier;
    //TOOO DOOOOOO Fix the accumulated Scores
    private List<float> accumulatedScores = new List<float>();

    public RewardUI reward;

    public ScreenFader screenFader;

    public GameObject CameraPositions;
    public Transform targetCameraTransform;

    public int cameraLevel;

    public CrowdController crowd;
    public float secondCountdown;

    void Awake()
    {
        score = 0f;
        multiplier = 1;
        eventIndex = 0;
        currentEvent = currentLevel.GetEventAtIndex(eventIndex);
        currentEventTime = 0.0f;
        nextEventTime = currentEvent.eventLength;

        for(int i = 0; i < GestureDisplays.Count; i++)
        {
            (GestureDisplays[i].GetComponent<GesturePrompt>()).SetGesture((Gesture)i);
        }
    }

    void Start()
    {
        instance = this;

        if(screenFader)
            screenFader.FadeIn();

        AudioSource audio = Camera.main.GetComponent<AudioSource>();
        audio.clip = currentLevel.GetSong();
        audio.PlayDelayed(DELAY_TILL_SONG_START);

        reward = GameObject.FindObjectOfType<RewardUI>();

        SetCameraLevel(0, true);

        crowd = GameObject.FindObjectOfType<CrowdController>();
        SetCrowd();
        secondCountdown = 0.0f;
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

        secondCountdown -= Time.deltaTime;
        if(secondCountdown <= 0.0f)
        {
            secondCountdown = 1f;
            accumulatedScores.Add(score);
        }

        // Progress level based on time
        if (t > nextEventTime)
        {
            eventIndex++;
            if (eventIndex < currentLevel.GetTotalEvents())
            {
                currentEvent = currentLevel.GetEventAtIndex(eventIndex);
                SetCrowd();
                currentEventTime = 0.0f;
                nextEventTime = currentEvent.eventLength;
            }
            else
                StartCoroutine(FinishSong());
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
                if (false && gestureEvent.gesture == Gesture.CrowdWave)
                {
                    if (currentEventTime > gestureEvent.eventLength * 0.4f && currentEventTime > gestureEvent.eventLength * 0.6f)
                        gestureEvent.LogGesture();
                }
                else
                {
                    gestureEvent.LogGesture();
                }

                //Add the score if the gesture is complete
                if (gestureEvent.EventFullfilled())
                {
                    score += gestureEvent.GetReward() * multiplier;
                    BroadcastReward();
                }
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
                if (poseEvent.EventFullfilled())
                {
                    multiplier += poseEvent.GetMultiplier();
                    AdvancePlayer();
                }
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
        if(cameraLevel >= MAX_LEVEL - 1)
        {
            return;
        }
        cameraLevel++;
        SetCameraLevel(cameraLevel, false);

        if(reward)
        {
            reward.Advance();
        }
    }

    public void SetCrowd()
    {
        if(currentEvent.GetEventType() == EventType.GESTURE_TYPE)
        {
            crowd.Do((currentEvent as GestureBeatMapEvent).gesture);
        }
        else
        {
            crowd.Pump(true, true);
        }
    }

    IEnumerator FinishSong()
    {
        StartCoroutine(screenFader.FadeOut());

        yield return new WaitForSeconds(0.5f);
        while (screenFader.fading)
        {
            yield return new WaitForSeconds(0.01f);
        }
        SceneManager.LoadScene(2);
        ResultsScreenTransitions.score = score;
        ResultsScreenTransitions.accumulatedScores = accumulatedScores;//DO THIS

        yield return null;
    }
}
