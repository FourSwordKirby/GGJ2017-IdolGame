using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSugoiLetter : MonoBehaviour {

    private Gradient color;
    private AnimationCurve colorCurve;
    private AnimationCurve yMovement;
    private AnimationCurve scale;

    private SpriteRenderer spriteRenderer;
    private float startTime;
    private Vector3 startPos;

	void Start () {
        startTime = Time.time;
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        float t = Time.time - startTime;

        spriteRenderer.color = color.Evaluate(colorCurve.Evaluate(t));
        transform.localPosition = startPos + Vector3.up * yMovement.Evaluate(t);
        transform.localScale = Vector3.one * scale.Evaluate(t);
	}

    public void SetParams(Gradient g, AnimationCurve c1, AnimationCurve c2, AnimationCurve c3)
    {
        color = g;
        colorCurve = c1;
        yMovement = c2;
        scale = c3;
    }
}
