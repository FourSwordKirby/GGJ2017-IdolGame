using System.Collections;
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

        Debug.Log("" + TwinStickControls.getLeftDirection() + " " + TwinStickControls.getRightDirection());

        Rect leftRect = LeftPanel.rect;
        Vector2 leftDir = TwinStickControls.getLeftDirection();
        LStickIcon.anchoredPosition =
            new Vector2(leftDir.x * leftRect.width / 2.0f, leftDir.y * leftRect.height / 2.0f);

        Rect rightRect = RightPanel.rect;
        Vector2 rightDir = TwinStickControls.getRightDirection();
        RStickIcon.anchoredPosition =
            new Vector2(rightDir.x * rightRect.width / 2.0f, rightDir.y * rightRect.height / 2.0f);
    }
}
