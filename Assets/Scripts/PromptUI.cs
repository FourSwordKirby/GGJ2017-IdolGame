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

	// Use this for initialization
	void Start () {
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

		if (currentPrompt != manager.GetPromptDisplay())
        {
            SpawnPrompt(manager.GetPromptDisplay());
        }
	}

    private void SpawnPrompt(RectTransform prompt)
    {
        if (promptPanel.childCount != 0)
        {
            Destroy(promptPanel.GetChild(0).gameObject);
        }
        
        if (!prompt)
        {
            return;
        }

        currentPrompt = prompt;
        RectTransform t = Instantiate(currentPrompt, promptPanel) as RectTransform;

        t.offsetMax = new Vector2(0.0f, 0.0f);
        t.offsetMin = new Vector2(0.0f, 0.0f);
    }
}
