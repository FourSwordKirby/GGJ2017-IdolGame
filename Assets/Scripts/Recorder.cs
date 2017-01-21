using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour {

    private ConcertManager manager;
    private List<float> times = new List<float>();
    private List<float> lengths = new List<float>();

	// Use this for initialization
	void Start () {
        manager = GameObject.FindObjectOfType<ConcertManager>();
        times.Add(0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            lengths.Add(manager.songTime - times[times.Count - 1]);
            times.Add(manager.songTime);
            Debug.Log("SPACE");
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            string timesStr = "Times: ";
            foreach (float f in times)
            {
                timesStr += "" + f + ", ";
            }
            string lengthsStr = "Lengths: ";
            foreach (float f in lengths)
            {
                lengthsStr += "" + f + ", ";
            }
            Debug.Log(timesStr + "\n" + lengthsStr);
        }
	}
}
