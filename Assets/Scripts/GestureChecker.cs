using UnityEngine;
using System.Collections;

public class GestureChecker : MonoBehaviour {

    public TwinStickControls twinStickControls;

    Pose maintainedPose = Pose.Neutral;
    Pose currentPose = Pose.Neutral;
    float poseTimer;
    float requiredHeldTime = 0.2f;

	// Update is called once per frame
	void Update () {
        //print("left" + TwinStickControls.getLeftDirection());
        //print("right" + TwinStickControls.getRightDirection());
        if (twinStickControls.CompletedLeftArmPumps() && twinStickControls.CompletedRightArmPumps())
        {
            Debug.Log("SimulPump detected");
            resetChecker();
            ConcertManager.instance.PerformGesture(Gesture.SimultaneousArmPumps);
        }
        else if (twinStickControls.CompletedLeftArmPumps())
        {
            resetChecker();
            ConcertManager.instance.PerformGesture(Gesture.LeftArmPumps);
        }
        else if (twinStickControls.CompletedRightArmPumps() || Input.GetKeyDown(KeyCode.Z))
        {
            resetChecker();
            ConcertManager.instance.PerformGesture(Gesture.RightArmPumps);
        }
        if (twinStickControls.CompletedSlowWave() || Input.GetKeyDown(KeyCode.X))
        {
            resetChecker();
            ConcertManager.instance.PerformGesture(Gesture.SlowWave);
        }
        if (twinStickControls.CompletedCrowdWave() || Input.GetKeyDown(KeyCode.C))
        {
            resetChecker();
            ConcertManager.instance.PerformGesture(Gesture.CrowdWave);
        }

        if (twinStickControls.CompletedClap())
        {
            resetChecker();
            ConcertManager.instance.PerformGesture(Gesture.Clap);
        }

        if (twinStickControls.RetrievePose() == Pose.Neutral)
        {
            maintainedPose = Pose.Neutral;
            poseTimer = 0.0f;
        }
        else if(currentPose != twinStickControls.RetrievePose())
        {
            poseTimer = 0.0f;
        }
        else if(maintainedPose != currentPose)
        {
            poseTimer += Time.deltaTime;
            if(poseTimer > requiredHeldTime)
            {
                maintainedPose = twinStickControls.RetrievePose();
                ConcertManager.instance.PerformPose(maintainedPose);
            }
        }
        currentPose = twinStickControls.RetrievePose();
    }

    void resetChecker()
    {
        twinStickControls.ClearBuffer();
    }
}
