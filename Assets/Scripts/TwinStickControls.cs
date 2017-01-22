using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TwinStickControls : Controls{
    /*These constants refer to specific thresholds for reading in inputs
     * For example, the constant FALL_THROUGH_THRESHOLD denotes the threshold 
     * between crouching on a platform and falling through the platform
     */
    public const float axisThreshold = 0.15f;
    public const float keyboardScaling = 2.0f;

    //List of positions and the time that position was recorded
    public List<KeyValuePair<Parameters.ControllerDirection, float>> leftPositions = new List<KeyValuePair<Parameters.ControllerDirection, float>>();
    public List<KeyValuePair<Parameters.ControllerDirection, float>> rightPositions = new List<KeyValuePair<Parameters.ControllerDirection, float>>();

    public static Vector2 getLeftDirection()
    {
        float xAxis = 0;
        float yAxis = 0;
        if (Mathf.Abs(Input.GetAxis("Left Horizontal")) > Mathf.Abs(Input.GetAxis("Left Keyboard Horizontal")))
        {
            xAxis = Input.GetAxis("Left Horizontal");

            if (Mathf.Abs(xAxis) < axisThreshold)
                xAxis = 0;
        }
        else
            xAxis = Mathf.Clamp(keyboardScaling * Input.GetAxis("Left Keyboard Horizontal"), -1, 1);
        if (Mathf.Abs(Input.GetAxis("Left Vertical")) > Mathf.Abs(Input.GetAxis("Left Keyboard Vertical")))
        {
            yAxis = Input.GetAxis("Left Vertical");

            if (Mathf.Abs(yAxis) < axisThreshold)
                yAxis = 0;
        }
        else
            yAxis = Mathf.Clamp(keyboardScaling * Input.GetAxis("Left Keyboard Vertical"), -1, 1);

        return new Vector2(xAxis, yAxis);
    }

    public static Vector2 getRightDirection()
    {
        float xAxis = 0;
        float yAxis = 0;
        if (Mathf.Abs(Input.GetAxis("Right Horizontal")) > Mathf.Abs(Input.GetAxis("Right Keyboard Horizontal")))
        {
            xAxis = Input.GetAxis("Right Horizontal");

            if (Mathf.Abs(xAxis) < axisThreshold)
                xAxis = 0;
        }
        else
            xAxis = Mathf.Clamp(keyboardScaling * Input.GetAxis("Right Keyboard Horizontal"), -1, 1);
        if (Mathf.Abs(Input.GetAxis("Right Vertical")) > Mathf.Abs(Input.GetAxis("Right Keyboard Vertical")))
        {
            yAxis = Input.GetAxis("Right Vertical");

            if (Mathf.Abs(yAxis) < axisThreshold)
                yAxis = 0;
        }
        else
            yAxis = Mathf.Clamp(keyboardScaling * Input.GetAxis("Right Keyboard Vertical"), -1, 1);

        return new Vector2(xAxis, yAxis);
    }

    float timer = 0;
    void Update()
    {
        timer += Time.deltaTime;
        leftPositions.Add(new KeyValuePair<Parameters.ControllerDirection, float>(Parameters.vectorToDirection(getLeftDirection()), timer));
        rightPositions.Add(new KeyValuePair<Parameters.ControllerDirection, float>(Parameters.vectorToDirection(getRightDirection()), timer));
        if (leftPositions.Count > 100)
            leftPositions.RemoveAt(0);
        if (rightPositions.Count > 100)
            rightPositions.RemoveAt(0);
    }

    override public void ClearBuffer()
    {
        leftPositions.RemoveAll(x => true);
        rightPositions.RemoveAll(x => true);
    }

    //These implementations are kind of a first pass atm. This way of handling the input buffer isn't the best tbh

    //Motion is a single tap up on the right stick
    override public bool CompletedRightArmPumps()
    {
        if(leftPositions.Count == 0 || rightPositions.Count == 0)
        {
            return false;
        }

        if (leftPositions[leftPositions.Count - 1].Key != Parameters.ControllerDirection.Neutral)
        {
            return false;
        }

        int lastIndex = rightPositions.Count - 1;
        int lastRaisedIndex = rightPositions.FindLastIndex(x => x.Key == Parameters.ControllerDirection.N);

        //Debug.Log(lastIndex);
        if (lastIndex > 0 && lastRaisedIndex > 0 && rightPositions[lastIndex].Key == Parameters.ControllerDirection.Neutral)
        {
            float lastTime = rightPositions[lastIndex].Value;
            float lastRaisedTime = rightPositions.FindLast(x => x.Key == Parameters.ControllerDirection.N).Value;
            float lastLoweredTime = rightPositions.GetRange(0, lastRaisedIndex).FindLast(x => x.Key == Parameters.ControllerDirection.Neutral).Value;

            //Debug.Log("raiseTime" + lastRaisedTime);
            //Debug.Log("lowerTime" + lastLoweredTime);

            if (lastRaisedTime <= lastLoweredTime)
                return false;

            if (lastTime - lastRaisedTime > 0.2f)
                return false;

            if (lastRaisedTime - lastLoweredTime > 0.2f)
                return false;

            return true;
        }

        return false;
    }

    override public bool CompletedLeftArmPumps()
    {
        if (leftPositions.Count == 0 || rightPositions.Count == 0)
        {
            return false;
        }

        if (rightPositions[rightPositions.Count - 1].Key != Parameters.ControllerDirection.Neutral)
        {
            return false;
        }

        int lastIndex = leftPositions.Count - 1;
        int lastRaisedIndex = leftPositions.FindLastIndex(x => x.Key == Parameters.ControllerDirection.N);

        //Debug.Log(lastIndex);
        if (lastIndex > 0 && lastRaisedIndex > 0 && leftPositions[lastIndex].Key == Parameters.ControllerDirection.Neutral)
        {
            float lastTime = leftPositions[lastIndex].Value;
            float lastRaisedTime = leftPositions.FindLast(x => x.Key == Parameters.ControllerDirection.N).Value;
            float lastLoweredTime = leftPositions.GetRange(0, lastRaisedIndex).FindLast(x => x.Key == Parameters.ControllerDirection.Neutral).Value;

            //Debug.Log("raiseTime" + lastRaisedTime);
            //Debug.Log("lowerTime" + lastLoweredTime);

            if (lastRaisedTime <= lastLoweredTime)
                return false;

            if (lastTime - lastRaisedTime > 0.2f)
                return false;

            if (lastRaisedTime - lastLoweredTime > 0.2f)
                return false;

            return true;
        }

        return false;
    }

    //Motion is a synchronized upper half-circles on both sticks
    override public bool CompletedSlowWave()
    {
        List<Parameters.ControllerDirection> rightWavePositions = new List<Parameters.ControllerDirection>() {Parameters.ControllerDirection.E,
                                                                                       Parameters.ControllerDirection.N,
                                                                                       Parameters.ControllerDirection.W };
        
        List<Parameters.ControllerDirection> leftWavePositions = new List<Parameters.ControllerDirection>() {Parameters.ControllerDirection.W,
                                                                                       Parameters.ControllerDirection.N,
                                                                                       Parameters.ControllerDirection.E };

        return ((motionDetected(rightWavePositions, leftPositions, 1.0f) && motionDetected(rightWavePositions, rightPositions, 1.0f))
                || (motionDetected(leftWavePositions, leftPositions, 1.0f) && motionDetected(leftWavePositions, rightPositions, 1.0f)));
    }

    //Motion is a synchronized upper half-circles on both sticks
    public bool CompletedLeftWave()
    {
        List<Parameters.ControllerDirection> rightWavePositions = new List<Parameters.ControllerDirection>() {Parameters.ControllerDirection.E,
                                                                                       Parameters.ControllerDirection.N,
                                                                                       Parameters.ControllerDirection.W };

        List<Parameters.ControllerDirection> leftWavePositions = new List<Parameters.ControllerDirection>() {Parameters.ControllerDirection.W,
                                                                                       Parameters.ControllerDirection.N,
                                                                                       Parameters.ControllerDirection.E };

        return (motionDetected(rightWavePositions, leftPositions, 1.0f) || motionDetected(leftWavePositions, leftPositions, 1.0f));
    }

    //Motion is a synchronized upper half-circles on both sticks
    public bool CompletedRightWave()
    {
        List<Parameters.ControllerDirection> rightWavePositions = new List<Parameters.ControllerDirection>() {Parameters.ControllerDirection.E,
                                                                                       Parameters.ControllerDirection.N,
                                                                                       Parameters.ControllerDirection.W };

        List<Parameters.ControllerDirection> leftWavePositions = new List<Parameters.ControllerDirection>() {Parameters.ControllerDirection.W,
                                                                                       Parameters.ControllerDirection.N,
                                                                                       Parameters.ControllerDirection.E };

        return (motionDetected(rightWavePositions, rightPositions, 1.0f) || motionDetected(leftWavePositions, rightPositions, 1.0f));
    }

    public override bool CompletedClap()
    {
        List<Parameters.ControllerDirection> left = new List<Parameters.ControllerDirection>()
        {
            Parameters.ControllerDirection.W,
            Parameters.ControllerDirection.E
        };

        List<Parameters.ControllerDirection> right = new List<Parameters.ControllerDirection>()
        {
            Parameters.ControllerDirection.E,
            Parameters.ControllerDirection.W
        };
        return (motionDetected(left, leftPositions, 1.0f)
            && motionDetected(right, rightPositions, 1.0f));
    }

    //Motion is hold down on both sticks and then flick up
    override public bool CompletedCrowdWave()
    {
        List<Parameters.ControllerDirection> crowdWavePositions = new List<Parameters.ControllerDirection>() {
            Parameters.ControllerDirection.S,
            Parameters.ControllerDirection.N };
        float[] holds = { 0.3f, 0.0f };

        bool left = motionDetected(crowdWavePositions, leftPositions, 5.0f, holds);
        bool right = motionDetected(crowdWavePositions, rightPositions, 5.0f, holds);

        if (left || right)
        {
            Debug.Log("Results: " + left + " " + right);
        }

        return left && right;
    }


    override public Pose RetrievePose()
    {
        Parameters.ControllerDirection leftStickDir = Parameters.vectorToDirection(getLeftDirection());
        Parameters.ControllerDirection rightStickDir = Parameters.vectorToDirection(getRightDirection());
        if ((leftStickDir == Parameters.ControllerDirection.N && rightStickDir == Parameters.ControllerDirection.S)
            || (leftStickDir == Parameters.ControllerDirection.S && rightStickDir == Parameters.ControllerDirection.N))
            return Pose.KillerQueen;
        if ((leftStickDir == Parameters.ControllerDirection.N && rightStickDir == Parameters.ControllerDirection.SE)
           || (leftStickDir == Parameters.ControllerDirection.SW && rightStickDir == Parameters.ControllerDirection.N))
            return Pose.PierceTheHeavens;
        if ((leftStickDir == Parameters.ControllerDirection.NE && rightStickDir == Parameters.ControllerDirection.NW))
            return Pose.Denial;
        if ((leftStickDir == Parameters.ControllerDirection.SW && rightStickDir == Parameters.ControllerDirection.SE))
            return Pose.Naruto;

        return Pose.Neutral;
    }

    bool motionDetected(List<Parameters.ControllerDirection> motion, List<KeyValuePair<Parameters.ControllerDirection, float>> handPositions, float upperBound, float[] holds = null)
    {
        bool debugme = false;
        int lastIndex = handPositions.Count - 1;
        if (lastIndex == -1)
            return false;

        for (int i = motion.Count - 1; i >= 0 ; i--)
        {
            Parameters.ControllerDirection position = motion[i];
            
            if (handPositions[lastIndex].Key != position)
            {
                return false;
            }

            if (holds != null && holds[i] > 0.0f)
            {
                float heldTime = 0.0f;
                for(int j = lastIndex - 1; j >= 0; j--)
                {
                    if(handPositions[j].Key == position)
                    {
                        heldTime = handPositions[lastIndex].Value - handPositions[j].Value;
                        if(heldTime > holds[i])
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                Debug.Log("" + i + " Held time for " + position + " was " + heldTime);
                if(heldTime < holds[i])
                {
                    return false;
                }
                Debug.Log("after");
                debugme = true;
            }

            if(i > 0)
            {
                int nextIndex = handPositions.GetRange(0, lastIndex).FindLastIndex(x => x.Key == motion[i - 1]);

                if (nextIndex == -1)
                {
                    return false;
                }

                float delay = handPositions[lastIndex].Value - handPositions[nextIndex].Value;
                if (delay >= upperBound)
                {
                    return false;
                }

                lastIndex = nextIndex;
            }
            if(debugme)
            {
                Debug.Log("After");
            }
        }
        return true;
    }
}
