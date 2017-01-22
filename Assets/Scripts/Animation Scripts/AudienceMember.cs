using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceMember : MonoBehaviour {

    public Gesture currentGesture;
    public GameObject leftArm;
    public GameObject rightArm;

    public Animator AudienceAnimator;
    public float ArmRestingHeight = 1.4f;
    public float ArmPumpingHeight = -0.6f;
    public float ArmSlowWaveHeight = 0.7f;
    public float ArmCrowdWaveScale = 0.7f;



    // Update is called once per frame
    void Update () {
        if(currentGesture == Gesture.Idle)
        {
            //leftArm.transform.position = Vector3.MoveTowards(leftArm.transform.position, );
        }
        else if(currentGesture == Gesture.LeftArmPumps)
        {

        }
	}
}
