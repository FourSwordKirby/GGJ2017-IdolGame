using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceMember : MonoBehaviour {

    public Gesture currentGesture;
    public GameObject leftArm;
    public GameObject rightArm;

    public Animator AudienceAnimator;
    private float ArmRestingHeight = 4.2f;
    private float ArmPumpingHeight = 4.2f;
    private float ArmSlowWaveHeight = 3.5f;

    private float ArmCrowdWaveScale = 0.75f;

    private bool InTransition;

    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            PerformGesture(Gesture.Idle);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            PerformGesture(Gesture.RightArmPumps);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            PerformGesture(Gesture.SlowWave);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            PerformGesture(Gesture.CrowdWave);
        }

        if (currentGesture == Gesture.CrowdWave)
        {
            if (AudienceAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
                PerformGesture(Gesture.Idle);
        }

        //Transitioning between animation states. Doesn't look that good so not really worth it
        //if(InTransition)
        //{
        //    if (currentGesture == Gesture.Idle)
        //    {
        //        if (leftArm.transform.localPosition.y == ArmRestingHeight && rightArm.transform.localPosition.y == ArmRestingHeight
        //            && leftArm.transform.up == Vector3.up && rightArm.transform.up == Vector3.up)
        //        {
        //            InTransition = false;
        //            AudienceAnimator.Play("AudienceIdle");
        //        }
        //        else
        //        {
        //            leftArm.transform.localPosition = Vector3.MoveTowards(leftArm.transform.localPosition, new Vector3(-0.9f, ArmRestingHeight, 0.05f), 0.03f);
        //            rightArm.transform.localPosition = Vector3.MoveTowards(rightArm.transform.localPosition, new Vector3(0.9f, ArmRestingHeight, 0.05f), 0.03f);
        //            leftArm.transform.rotation = Quaternion.RotateTowards(leftArm.transform.rotation, Quaternion.Euler(0, 0, 0), 3f);
        //            rightArm.transform.rotation = Quaternion.RotateTowards(rightArm.transform.rotation, Quaternion.Euler(0, 0, 0), 3f);
        //        }
        //    }
        //    else if (currentGesture == Gesture.RightArmPumps)
        //    {
        //        if (leftArm.transform.localPosition.y == ArmRestingHeight && rightArm.transform.localPosition.y == ArmPumpingHeight
        //            && leftArm.transform.rotation == Quaternion.Euler(0, 0, 0) && rightArm.transform.rotation == Quaternion.Euler(179, 0, 0))
        //            PlayAnimation("AudienceRightArmPump");
        //        else
        //        {
        //            leftArm.transform.localPosition = Vector3.MoveTowards(leftArm.transform.localPosition, new Vector3(-0.9f, ArmRestingHeight, 0.05f), 0.03f );
        //            rightArm.transform.localPosition = Vector3.MoveTowards(rightArm.transform.localPosition, new Vector3(0.9f, ArmPumpingHeight, 0.05f), 0.03f);
        //            leftArm.transform.rotation = Quaternion.RotateTowards(leftArm.transform.rotation, Quaternion.Euler(0, 0, 0), 3f);
        //            rightArm.transform.rotation = Quaternion.RotateTowards(rightArm.transform.rotation, Quaternion.Euler(179, 0, 0), 3f);
        //        }
        //    }
        //}
    }

    void PerformGesture(Gesture g)
    {
        AudienceAnimator.enabled = false;
        currentGesture = g;
        if (currentGesture == Gesture.Idle)
            PlayAnimation("AudienceIdle");
        if (currentGesture == Gesture.LeftArmPumps)
            PlayAnimation("AudienceLeftArmPump");
        if (currentGesture == Gesture.RightArmPumps)
            PlayAnimation("AudienceRightArmPump");
        if (currentGesture == Gesture.SimultaneousArmPumps)
            PlayAnimation("AudienceBothArmPump");
        if (currentGesture == Gesture.SlowWave)
            PlayAnimation("AudienceBothArmWave");
        if (currentGesture == Gesture.CrowdWave)
            PlayAnimation("AudienceCrowdWave");
    }

    void PlayAnimation(string animName)
    {
        InTransition = false;
        AudienceAnimator.enabled = true;
        AudienceAnimator.Play(animName);
    }
}
