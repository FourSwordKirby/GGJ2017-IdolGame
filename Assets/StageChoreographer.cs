using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageChoreographer : MonoBehaviour
{

    public GameObject stageLights;
    public GameObject pinwheels;
    public GameObject hexagons;
    public GameObject fire;
    public GameObject fireworks;

    public Light globalLight;
    public Light stageLight;

    public float timer;

    float globalLightHigh = 0.1f;
    float globalLightMed = 0.5f;
    float globalLightLow = 0.05f;
    float globalLightOff = 0f;

    float stageLightHigh = 0.1f;
    float stageLightMed = 0.5f;
    float stageLightLow = 0.05f;
    float stageLightOff = 0f;

    private void Start()
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float previousTime = timer;
        timer += Time.deltaTime;
        if (previousTime < 3.5f && timer < 3.5f)
        {
            stageLights.SetActive(false);
            pinwheels.SetActive(false);
            hexagons.SetActive(false);
            fire.SetActive(false);
            fireworks.SetActive(false);

            fire.SetActive(true);

            StartCoroutine(changeGlobalIntensity(globalLight.intensity, globalLightLow));
            StartCoroutine(changeSpotIntensity(stageLight.intensity, stageLightLow));
        }
        else if (previousTime < 3.5f && timer >= 3.5f)
        {
            stageLights.SetActive(true);
        }
        else if (previousTime < 7f && timer >= 7f)
        {
            fire.SetActive(false);
            pinwheels.SetActive(true);
        }
        else if (previousTime < 8f && timer >= 8f)
        {
            pinwheels.SetActive(false);
            stageLights.SetActive(true);
            hexagons.SetActive(true);
            fire.SetActive(true);
            StartCoroutine(changeGlobalIntensity(globalLight.intensity, globalLightMed));
            StartCoroutine(changeSpotIntensity(stageLight.intensity, stageLightMed));
        }
        else if (previousTime < 16f && timer >= 16f)
        {
            pinwheels.SetActive(true);
            fireworks.SetActive(true);

        }
        else if (previousTime < 23f && timer >= 23f)
        {
            pinwheels.SetActive(false);
            stageLights.SetActive(false);
            fireworks.SetActive(false);
            StartCoroutine(changeGlobalIntensity(globalLight.intensity, globalLightLow));
            fire.SetActive(false);

        }
        else if (previousTime < 28f && timer >= 28f)
        {
            fireworks.SetActive(true);
        }
        else if (previousTime < 30f && timer >= 30f)
        {
            fireworks.SetActive(false);
            stageLights.SetActive(true);
            hexagons.SetActive(false);
            fire.SetActive(true);
            StartCoroutine(changeGlobalIntensity(globalLight.intensity, globalLightHigh));
        }
        else if (previousTime < 35f && timer >= 35f)
        {
            fireworks.SetActive(true);
        }
        else if (previousTime < 37f && timer >= 37f)
        {
            StartCoroutine(changeGlobalIntensity(globalLight.intensity, globalLightLow));
            StartCoroutine(changeSpotIntensity(stageLight.intensity, stageLightLow));
            stageLights.SetActive(false);
            hexagons.SetActive(true);
            fire.SetActive(false);
        }
        else if (previousTime < 42f && timer >= 42f)
        {
            pinwheels.SetActive(true);
        }
        else if (previousTime < 44f && timer >= 44f)
        {
            pinwheels.SetActive(false);
        }
        else if (previousTime < 52f && timer >= 52f)
        {
            hexagons.SetActive(false);
            StartCoroutine(changeGlobalIntensity(globalLight.intensity, globalLightOff));
            StartCoroutine(changeSpotIntensity(stageLight.intensity, stageLightOff));
        }
        else if (previousTime < 54f && timer >= 54f)
        {
            StartCoroutine(changeGlobalIntensity(globalLight.intensity, globalLightMed));
            StartCoroutine(changeSpotIntensity(stageLight.intensity, stageLightHigh));
        }
        else if (previousTime < 59f && timer >= 59f)
        {
            pinwheels.SetActive(true);
        }
        else if (previousTime < 61f && timer >= 61f)
        {
            pinwheels.SetActive(false);
            fire.SetActive(true);
        }
        else if (previousTime < 68f && timer >= 68f)
        {
            StartCoroutine(changeGlobalIntensity(globalLight.intensity, globalLightOff));
            StartCoroutine(changeSpotIntensity(stageLight.intensity, stageLightOff));
            fire.SetActive(false);
        }
        else if (previousTime < 70f && timer >= 70f)
        {
            StartCoroutine(changeGlobalIntensity(globalLight.intensity, globalLightLow));
            StartCoroutine(changeSpotIntensity(stageLight.intensity, stageLightLow));
            hexagons.SetActive(true);
            stageLights.SetActive(true);
        }
        else if (previousTime < 76f && timer >= 76f)
        {
            fireworks.SetActive(true);
        }
        else if (previousTime < 78f && timer >= 78f)
        {
            fireworks.SetActive(false);
            fire.SetActive(true);
            StartCoroutine(changeGlobalIntensity(globalLight.intensity, globalLightHigh));
        }
        else if (previousTime < 83f && timer >= 83f)
        {
            fireworks.SetActive(true);
            fire.SetActive(false);
            hexagons.SetActive(false);
        }
        else if (previousTime < 85f && timer >= 85f)
        {
            stageLights.SetActive(true);
            hexagons.SetActive(true);
            fire.SetActive(true);
            fireworks.SetActive(true);
        }
        else if (previousTime < 91f && timer >= 91f)
        {
            pinwheels.SetActive(true);
        }
        else if (previousTime < 98f && timer >= 98f)
        {
            stageLights.SetActive(false);
            pinwheels.SetActive(false);
            fire.SetActive(false);
            fireworks.SetActive(false);
            StartCoroutine(changeGlobalIntensity(globalLight.intensity, globalLightLow));
            StartCoroutine(changeSpotIntensity(stageLight.intensity, stageLightLow));
        }
        else if (previousTime < 103f && timer >= 103f)
        {
            fireworks.SetActive(true);
        }
        else if (previousTime < 105f && timer >= 105f)
        {
            fireworks.SetActive(false);
            StartCoroutine(changeGlobalIntensity(globalLight.intensity, globalLightOff));
            StartCoroutine(changeSpotIntensity(stageLight.intensity, stageLightOff));
        }
    }

    IEnumerator changeGlobalIntensity(float oldIntensity, float newIntensity)
    {
        float timer = 0.0f;
        float lerpTime = 1.0f;

        while (timer < lerpTime)
        {
            timer += Time.deltaTime;
            globalLight.intensity = Mathf.Lerp(oldIntensity, newIntensity, timer / lerpTime);
            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
    }

    IEnumerator changeSpotIntensity(float oldIntensity, float newIntensity)
    {
        float timer = 0.0f;
        float lerpTime = 1.0f;

        while (timer < lerpTime)
        {
            timer += Time.deltaTime;
            stageLight.intensity = Mathf.Lerp(oldIntensity, newIntensity, timer / lerpTime);
            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
    }
}
