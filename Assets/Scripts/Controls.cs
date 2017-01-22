using UnityEngine;
using System.Collections;

public abstract class Controls:MonoBehaviour {

    public abstract void ClearBuffer();

    public abstract bool CompletedRightArmPumps();
    public abstract bool CompletedLeftArmPumps();
    public abstract bool CompletedClap();

    public abstract bool CompletedSlowWave();

    public abstract bool CompletedCrowdWave();

    public abstract Pose RetrievePose();
}
