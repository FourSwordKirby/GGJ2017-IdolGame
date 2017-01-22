using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightRotate : MonoBehaviour {
    public AnimationCurve curve;
    public float angle;
    void Update()
    {
        transform.localEulerAngles = Vector3.forward * (0.5f * angle * curve.Evaluate(Time.time));
    }
}
