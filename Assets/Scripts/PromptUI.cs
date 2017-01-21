using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptUI : MonoBehaviour {

    public ConcertManager manager;

    private RectTransform promptPanel;
    private RectTransform currentPrompt;

	// Use this for initialization
	void Start () {
        if (!manager)
        {
            manager = GameObject.FindObjectOfType<ConcertManager>();
        }
        promptPanel = this.transform.FindChild("PromptPanel").GetComponent<RectTransform>();
        SpawnPrompt();
    }
	
	// Update is called once per frame
	void Update () {
		if (currentPrompt != manager.GetPromptDisplay())
        {
            Destroy(promptPanel.GetChild(0).gameObject);
            SpawnPrompt();
        }
	}

    private void SpawnPrompt()
    {
        currentPrompt = manager.GetPromptDisplay();
        RectTransform t = Instantiate<RectTransform>(currentPrompt, promptPanel);
        t.offsetMax = new Vector2(0.0f, 0.0f);
        t.offsetMin = new Vector2(0.0f, 0.0f);
    }
}
