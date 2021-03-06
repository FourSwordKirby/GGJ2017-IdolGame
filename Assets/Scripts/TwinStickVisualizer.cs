﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinStickVisualizer : MonoBehaviour {

    public TwinStickControls controls;
    public RectTransform LeftPanel;
    public RectTransform RightPanel;
    public RectTransform LStickIcon;
    public RectTransform RStickIcon;

    // Use this for initialization
    void Start () {
		if (!controls)
        {
            controls = GameObject.FindObjectOfType<TwinStickControls>();
            if(!controls)
            {
                Debug.LogError("Can't find TwinStick controls");
            }
        }
        LeftPanel = this.transform.FindChild("Left").GetComponent<RectTransform>();
        RightPanel = this.transform.FindChild("Right").GetComponent<RectTransform>();
        LStickIcon = LeftPanel.FindChild("LStick").GetComponent<RectTransform>();
        RStickIcon = RightPanel.FindChild("RStick").GetComponent<RectTransform>();

    }
	
	// Update is called once per frame
	void Update () {
        Rect leftRect = LeftPanel.rect;
        Vector2 leftDir = TwinStickControls.getLeftDirection().normalized;
        LStickIcon.anchoredPosition = leftDir * leftRect.width / 2.0f;

        Rect rightRect = RightPanel.rect;
        Vector2 rightDir = TwinStickControls.getRightDirection().normalized;
        RStickIcon.anchoredPosition = rightDir * rightRect.width / 2.0f;
    }
}
