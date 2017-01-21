using UnityEngine;
using System.Collections;

public class GestureChecker : MonoBehaviour {

    public TwinStickControls twinStickControls;

	// Update is called once per frame
	void Update () {
        //print("left" + TwinStickControls.getLeftDirection());
        //print("right" + TwinStickControls.getRightDirection());
        if (twinStickControls.CompletedArmPumps())
        {
            resetChecker();
            print("Arms Pumped");
        }
        //Do a thing wh
    }

    void resetChecker()
    {
        twinStickControls.ClearBuffer();
    }
}
