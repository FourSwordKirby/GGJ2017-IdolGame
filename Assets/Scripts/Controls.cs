using UnityEngine;
using System.Collections;

public abstract class Controls:MonoBehaviour {

    public abstract void ClearBuffer();

    public abstract bool CompletedArmPumps();

    public abstract bool CompletedSlowWave();

    public abstract bool CompletedCrowdWave();
}
