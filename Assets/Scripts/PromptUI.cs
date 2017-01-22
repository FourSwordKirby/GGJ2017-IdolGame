using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptUI : MonoBehaviour {

    public ConcertManager manager;

    public RectTransform ReadyPrompt;
    public RectTransform StartPrompt;

    private RectTransform promptPanel;
    private RectTransform currentPrompt;

    private Text timePanel;

    public BeatMapEvent currentEvent;
    public GesturePrompt gesturePrompt;
    public RectTransform currentTransform;

	// Use this for initialization
	void Start () {
        currentEvent = null;

        if (!manager)
        {
            manager = GameObject.FindObjectOfType<ConcertManager>();
        }
        promptPanel = this.transform.FindChild("PromptPanel").GetComponent<RectTransform>();
        SpawnPrompt(null);

        timePanel = this.transform.FindChild("Time").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        timePanel.text = string.Format("Game Time: {0} \n Song Time: {1} \n Current Event Time: {2}",
            manager.gameTime,
            manager.songTime,
            manager.currentEventTime);

        timePanel.text += "\nScore: " + manager.score;

        if (manager.GetTimeTillFirstEvent() > 0)
        {
            float t = manager.GetTimeTillFirstEvent();
            if(t < 2.0)
            {
                SpawnPrompt(StartPrompt);
            }
            else if (t < 5.0)
            {
                SpawnPrompt(ReadyPrompt);
            }
            return;
        }

		if (currentEvent != manager.currentEvent)
        {
            currentEvent = manager.currentEvent;
            Debug.Log("Prompt change.");
            if(currentEvent is GestureBeatMapEvent)
            {
                GestureBeatMapEvent gbme = currentEvent as GestureBeatMapEvent;
                SpawnPrompt(gesturePrompt.GetComponent<RectTransform>(), (int)gbme.gesture);
            }
            else
            {
                SpawnPrompt(manager.GetPromptDisplay());
            }
        }
        
    }

    private void SpawnPrompt(RectTransform prompt, int gesture = -1)
    {
        if (promptPanel.childCount != 0)
        {
            GameObject c = promptPanel.GetChild(0).gameObject;
            GesturePrompt gp = c.GetComponent<GesturePrompt>();
            if (gp != null)
            {
                Debug.Log("Die triggered");
                gp.Die();
            }
            else
            {
                Destroy(promptPanel.GetChild(0).gameObject);
            }
        }
        
        if (!prompt)
        {
            return;
        }

        currentPrompt = prompt;
        RectTransform t = Instantiate(currentPrompt) as RectTransform;
        t.SetParent(promptPanel, false);
        
        if(gesture > -1)
        {
            t.GetComponent<GesturePrompt>().SetGesture((Gesture)gesture);
        }
    }
}
