using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleColumnRotate : MonoBehaviour
{

    public AnimationCurve curve;
    public float angle;
    
    void Start()
    {

    }
    
    void Update()
    {
        transform.localEulerAngles = Vector3.right * (0.5f * angle * curve.Evaluate(Time.time) - 90);
    }
}
