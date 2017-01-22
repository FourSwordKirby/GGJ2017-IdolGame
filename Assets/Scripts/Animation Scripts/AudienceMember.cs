using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceMember : MonoBehaviour {

    public Gesture currentGesture;
    public GameObject leftArm;
    public GameObject rightArm;

    public Color leftGlowstickColor;
    public Color rightGlowstickColor;
    public float colorChangeTime;
    public Color currentLeftGlowstickColor;
    public Color currentRightGlowstickColor;
    private float leftTimer;
    private float rightTimer;

    public SpriteRenderer leftGlowstick;
    public SpriteRenderer rightGlowstick;

    public Animator AudienceAnimator;

    public float crowdWaveDelay;
    //private bool InTransition;
    //private float ArmRestingHeight = 4.2f;
    //private float ArmPumpingHeight = 4.2f;
    //private float ArmSlowWaveHeight = 3.5f;
    //private float ArmCrowdWaveScale = 0.75f;


    // Update is called once per frame
    void Update () {
        //Make the glowsticks the desired color
        if(leftGlowstick.color != leftGlowstickColor)
        {
            leftTimer += Time.deltaTime;
            leftGlowstick.color = Color.Lerp(currentLeftGlowstickColor, leftGlowstickColor, leftTimer / colorChangeTime);
            if(leftTimer > colorChangeTime)
            {
                currentLeftGlowstickColor = leftGlowstickColor;
                leftTimer = 0;
            }
        }
        if (rightGlowstick.color != rightGlowstickColor)
        {
            rightTimer += Time.deltaTime;
            rightGlowstick.color = Color.Lerp(currentRightGlowstickColor, rightGlowstickColor, rightTimer / colorChangeTime);
            if (rightTimer > colorChangeTime)
            {
                currentRightGlowstickColor = rightGlowstickColor;
                rightTimer = 0;
            }
        }

        if (currentGesture == Gesture.CrowdWave)
        {
            crowdWaveDelay -= Time.deltaTime;
            if (crowdWaveDelay < 0)
                PlayAnimation("AudienceCrowdWave");

            //Bug where we can't transition to crowdwave from idle
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

    public void PerformGesture(Gesture g, bool crowdWaveRight = true)
    {
        currentGesture = g;
        if (currentGesture == Gesture.Idle)
            PlayAnimation("AudienceIdle");
        if (currentGesture == Gesture.LeftArmPumps)
            PlayAnimation("AudienceLeftArmPump");
        if (currentGesture == Gesture.RightArmPumps)
            PlayAnimation("AudienceRightArmPump");
        if (currentGesture == Gesture.SimultaneousArmPumps)
            PlayAnimation("AudienceBothArmPump");
        if (currentGesture == Gesture.LeftWave)
            PlayAnimation("AudienceLeftArmWave");
        if (currentGesture == Gesture.RightWave)
            PlayAnimation("AudienceRightArmWave");
        if (currentGesture == Gesture.SlowWave)
            PlayAnimation("AudienceBothArmWave");
        if (currentGesture == Gesture.Clap)
            PlayAnimation("AudienceClap");
        if (currentGesture == Gesture.CrowdWave)
        {
            float crowdWaveLength = 2.0f; //This influences how long the wave lasts;
            if (crowdWaveRight)
                crowdWaveDelay = crowdWaveLength * (this.transform.position.x + 25)/50;
            else
                crowdWaveDelay = crowdWaveLength * -(this.transform.position.x - 25) / 50;
        }
    }

    void PlayAnimation(string animName)
    {
        //InTransition = false;
        //AudienceAnimator.enabled = true;
        AudienceAnimator.Play(animName);
    }
}
