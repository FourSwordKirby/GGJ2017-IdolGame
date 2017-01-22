using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCoreographer : MonoBehaviour {

    public GameObject spotlights;
    public GameObject pinwheels;
    public GameObject hexagons;
    public GameObject fire;
    public GameObject fireworks;

    public Light globalLight;
    public Light stageLight;

    public float timer;

	// Update is called once per frame
	void Update () {
        float previousTime = timer;
        timer += Time.deltaTime;

        if(previousTime < 3.5f && timer >= 3.5f)
        {
            spotlights.SetActive(true);
            spotlights.SetActive(true);
        }

    }
}
