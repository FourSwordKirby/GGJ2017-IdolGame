using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TwinStickControls : Controls{
    /*These constants refer to specific thresholds for reading in inputs
     * For example, the constant FALL_THROUGH_THRESHOLD denotes the threshold 
     * between crouching on a platform and falling through the platform
     */
    public const float axisThreshold = 0.15f;
    public const float keyboardScaling = 2.0f;

    //List of positions and the time that position was recorded
    public List<KeyValuePair<Vector2, float>> leftPositions = new List<KeyValuePair<Vector2, float>>();
    public List<KeyValuePair<Vector2, float>> rightPositions = new List<KeyValuePair<Vector2, float>>();

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
        leftPositions.Add(new KeyValuePair<Vector2, float>(getLeftDirection(), timer));
        rightPositions.Add(new KeyValuePair<Vector2, float>(getRightDirection(), timer));
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

    override public bool CompletedArmPumps()
    {
        int lastIndex = rightPositions.Count - 1;
        int lastRaisedIndex = rightPositions.FindLastIndex(x => x.Key.y == 1);

        //Debug.Log(lastIndex);
        if (lastIndex > 0 && lastRaisedIndex > 0 && rightPositions[lastIndex].Key.y == 0)
        {
            float lastTime = rightPositions[lastIndex].Value;
            float lastRaisedTime = rightPositions.FindLast(x => x.Key.y == 1).Value;
            float lastLoweredTime = rightPositions.GetRange(0, lastRaisedIndex).FindLast(x => x.Key.y == 0).Value;

            //Debug.Log("raiseTime" + lastRaisedTime);
            //Debug.Log("lowerTime" + lastLoweredTime);

            if (lastRaisedTime <= lastLoweredTime)
                return false;

            if (lastTime - lastRaisedTime > 0.5f)
                return false;

            if (lastRaisedTime - lastLoweredTime > 0.5f)
                return false;

            return true;
        }

        return false;
    }

    override public bool CompletedSlowWave()
    {
        List<Vector2> rightWavePositions;
        List<Vector2> leftWavePositions;
        return false;
    }

    override public bool CompletedCrowdWave()
    {
        return false;
    }
}
