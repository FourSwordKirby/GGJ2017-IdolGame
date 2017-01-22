using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeat : MonoBehaviour {

    public GameObject Heart;

    public float timer;
    private float beatLength = 1.75f;
    private float pause1 = 0.8f;
    private float pause2 = 0.9f;
    private float pause3 = 1.3f;

    // Update is called once per frame
    void Update () {
        float oldTimer = timer;
        timer += Time.deltaTime;

        if (oldTimer < pause1 && timer >= pause1)
            StartCoroutine(BeatHeart(1.1f));
        else if (oldTimer < pause2 && timer >= pause2)
            StartCoroutine(BeatHeart(1.1f));
        else if (oldTimer < pause3 && timer >= pause3)
            StartCoroutine(BeatHeart(1.3f));

        if (timer >= beatLength)
            timer = 0;
    }

    public IEnumerator BeatHeart(float size)
    {
        float sizeTimer = 0;
        float beatLength = 0.1f;
        while(sizeTimer < 2* beatLength)
        {
            sizeTimer += Time.deltaTime;
            if (sizeTimer < beatLength)
            {
                Heart.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * size, sizeTimer / beatLength);
                yield return new WaitForSeconds(0.01f);
            }
            else if (sizeTimer < 2 * beatLength)
            {
                Heart.transform.localScale = Vector3.Lerp(Vector3.one * size, Vector3.one, (sizeTimer - beatLength) / beatLength);
                yield return new WaitForSeconds(0.01f);
            }
        }
        yield return null;
    }
}
