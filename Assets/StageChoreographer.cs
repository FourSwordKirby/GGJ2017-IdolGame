using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageChoreographer : MonoBehaviour {

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
    if(previousTime == 0f)
        {
            fire.SetActive(true);
        }
    if(previousTime < 3.5f && timer >= 3.5f)
        {
            spotlights.SetActive(true);
        }
    if(previousTime < 7f && timer >= 7f)
        {
            fire.SetActive(false);
            pinwheels.SetActive(true);
        }
    if(previousTime < 8f && timer >= 8f)
        {
            pinwheels.SetActive(false);
            spotlights.SetActive(true);
            hexagons.SetActive(true);
            fire.SetActive(true);
            //globalLight.intensity = 0.1f;
            //stageLight.intensity = 0.1f;
        }
    if(previousTime < 16f && timer >= 16f)
        {
            pinwheels.SetActive(true);
            fireworks.SetActive(true);

        }
    if(previousTime < 23f && timer >= 23f)
        {
            pinwheels.SetActive(false);
            spotlights.SetActive(false);
            fireworks.SetActive(false);
            //globalLight.intensity = 0.05f;
            fire.SetActive(false);
            

        }
    if(previousTime < 28f && timer >= 28f)
        {
            fireworks.SetActive(true);
        }
    if(previousTime < 30f && timer >= 30f)
        {
            fireworks.SetActive(false);
            spotlights.SetActive(true);
            hexagons.SetActive(false);
            fire.SetActive(true);
            //globalLight.intensity = 0.5f;
        }
    if(previousTime < 35f && timer >= 35f)
        {
            fireworks.SetActive(true);
        }
    if(previousTime < 37f && timer >= 37f)
        {
            //globalLight.intensity = 0.05f;
            //spotLight.intensity = 0.05f;
            spotlights.SetActive(false);
            hexagons.SetActive(true);
            fire.SetActive(false);
        }
        if (previousTime < 42f && timer >= 42f)
        {
            pinwheels.SetActive(true);
        }
    if(previousTime < 44f && timer >= 44f)
        {
            pinwheels.SetActive(false);
        }
    if(previousTime < 52f && timer >= 52f)
        {
            hexagons.SetActive(false);
        }
    if(previousTime < 54f && timer >= 54f)
        {
            //globalLight.intensity = 0.1f;
            //spotLight.intensity = 0.1f;
        }
        if (previousTime < 59f && timer >= 59f)
        {
            pinwheels.SetActive(true);
        }
    if(previousTime < 61f && timer >= 61f)
        {
            pinwheels.SetActive(false);
            fire.SetActive(true);
        }
    if(previousTime < 68f && timer >= 68f)
        {
            fire.SetActive(false);
        }
    if(previousTime < 70f && timer >= 70f)
        {
            //globalLight.intensity = 0.05f;
            //spotLight.intensity = 0.05f;
            hexagons.SetActive(true);
            spotlights.SetActive(true);
        }
        if (previousTime < 76f && timer >= 76f)
        {
            fireworks.SetActive(true);
        }
    if(previousTime < 78f && timer >= 78f)
        {
            fireworks.SetActive(false);
            fire.SetActive(true);
            //globalLight.intensity = 0.5f;
        }
    if(previousTime < 83f && timer >= 83f)
        {
            fireworks.SetActive(true);
            fire.SetActive(false);
            hexagons.SetActive(false);
        }
    if(previousTime < 85f && timer >= 85f)
        {
            spotlights.SetActive(true);
            hexagons.SetActive(true);
            fire.SetActive(true);
            fireworks.SetActive(true);
        }
    if(previousTime < 91f && timer >= 91f)
        {
            pinwheels.SetActive(true);
        }
    if(previousTime < 98f && timer >= 98f)
        {
            spotlights.SetActive(false);
            pinwheels.SetActive(false);
            fire.SetActive(false);
            fireworks.SetActive(false);
            //globalLight.intensity = 0.05f;
            //spotLight.intensity = 0.05f;
        }
        if (previousTime < 103f && timer >= 103f)
        {
            fireworks.SetActive(true);
        }
    if(previousTime < 105f && timer >= 105f)
        {
            fireworks.SetActive(false);   
        }

    }
}
