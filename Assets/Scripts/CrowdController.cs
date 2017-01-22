using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdController : MonoBehaviour {

    public AudienceMember audienceMemberPrefab;
    public int attendenceCount;
    public bool waveRight;


    private List<AudienceMember> audienceMembers;

    private void Awake()
    {
        List<Vector3> SpawnPositions = new List<Vector3>()
        {
            new Vector3(0, 1.165417f, -40.51f),
            new Vector3(5.03f, 1.165417f, -32.49f),
            new Vector3(5.9f, 1.165417f, -20.4f),
            new Vector3(1.9f, 1.165417f, -13.6f)
        };

        audienceMembers = new List<AudienceMember>();
        for (int i = 0; i < attendenceCount; i++)
        {
            AudienceMember member = Instantiate(audienceMemberPrefab);
            float xPos;
            float zPos = Random.Range(-7.0f, -36.0f);
            if (zPos > -17f)
                xPos = Random.Range(-14.0f, 14.0f);
            else
            {
                float xRange = Mathf.Pow((500 - Mathf.Pow((zPos + 17), 2.0f)), 0.5f);
                xPos = Mathf.Sign(Random.Range(-100.0f, 100.0f)) * Random.Range(0f, xRange);
            }

            foreach(Vector3 pos in SpawnPositions)
            {
                if (Vector3.Distance(new Vector3(xPos, 0, zPos), pos) < 3.0f)
                {
                    zPos -= 5;
                }
            }

            member.transform.position = new Vector3(xPos, 0, zPos);
            audienceMembers.Add(member);
        }
    }

    // Use this for initialization
    void Start () {
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Idle();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Pump(false, true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Wave(true, true);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            CrowdWave();
        }
    }

    public void Idle()
    {
        foreach (AudienceMember member in audienceMembers)
            member.PerformGesture(Gesture.Idle);
    }

    public void Pump(bool left, bool right)
    {
        foreach (AudienceMember member in audienceMembers)
            member.PerformGesture(right ? left ? Gesture.SimultaneousArmPumps : Gesture.RightArmPumps : Gesture.LeftArmPumps);
    }

    public void Wave(bool left, bool right)
    {
        foreach (AudienceMember member in audienceMembers)
            member.PerformGesture(left ? right ? Gesture.SlowWave : Gesture.LeftWave : Gesture.RightWave);
    }

    public void CrowdWave()
    {
        waveRight = (Random.Range(-100.0f, 100.0f) > 0);
        foreach (AudienceMember member in audienceMembers)
            member.PerformGesture(Gesture.CrowdWave, waveRight);
    }

    public void Do(Gesture g)
    {
        if(g == Gesture.CrowdWave)
        {
            CrowdWave();
        }
        else
        {
            foreach (AudienceMember member in audienceMembers)
                member.PerformGesture(g);
        }
    }
}
