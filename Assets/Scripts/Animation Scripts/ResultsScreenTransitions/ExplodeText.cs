using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplodeText : MonoBehaviour
{
    public Gradient color;
    public AnimationCurve curve;

    private float startTime;
    private Text text;

    void Start()
    {
        startTime = Time.time;
        text = GetComponent<Text>();
    }
    
    void Update()
    {
        float t = Time.time - startTime;
        text.color = color.Evaluate(curve.Evaluate(t));
        text.transform.localScale = Vector3.one * (1 + curve.Evaluate(t) * 5);
    }
}
