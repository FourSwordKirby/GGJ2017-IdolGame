using UnityEngine;
using System.Collections;

public class GestureChecker : MonoBehaviour {

    public TwinStickControls twinStickControls;

	// Update is called once per frame
	void Update () {
        //print("left" + TwinStickControls.getLeftDirection());
        //print("right" + TwinStickControls.getRightDirection());
        if (twinStickControls.CompletedArmPumps() || Input.GetKeyDown(KeyCode.Z))
        {
            resetChecker();
            ConcertManager.instance.PerformGesture(Gesture.ArmPumps);
        }
        if (twinStickControls.CompletedSlowWave() || Input.GetKeyDown(KeyCode.X))
        {
            resetChecker();
            ConcertManager.instance.PerformGesture(Gesture.SlowWave);
        }
        if (twinStickControls.CompletedCrowdWave() || Input.GetKeyDown(KeyCode.C))
        {
            resetChecker();
            ConcertManager.instance.PerformGesture(Gesture.SlowWave);
        }
    }

    void resetChecker()
    {
        twinStickControls.ClearBuffer();
    }
}
